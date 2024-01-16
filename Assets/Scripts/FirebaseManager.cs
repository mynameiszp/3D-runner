using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirebaseManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    [SerializeField] private DependencyStatus dependencyStatus;
    [SerializeField] private FirebaseAuth auth;
    [SerializeField] private FirebaseUser User;
    [SerializeField] private DatabaseReference DBReference;

    //Login variables
    [Header("Login")]
    [SerializeField] private TMP_InputField emailLoginField;
    [SerializeField] private TMP_InputField passwordLoginField;
    [SerializeField] private TMP_Text warningLoginText;

    //Register variables
    [Header("Register")]
    [SerializeField] private TMP_InputField usernameRegisterField;
    [SerializeField] private TMP_InputField emailRegisterField;
    [SerializeField] private TMP_InputField passwordRegisterField;
    [SerializeField] private TMP_InputField passwordRegisterVerifyField;
    [SerializeField] private TMP_Text warningRegisterText;

    private string _username;
    private int _score;
    public static FirebaseManager Instance { get; private set; }
    private Score scoreManager;
    private Dictionary<string, int> leaders = new Dictionary<string, int>();
    void Awake()
    {
        if (Instance == null) Instance = this;
        scoreManager = Score.Instance;
        //Check that all of the necessary dependencies for Firebase are present on the system
      
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    //If they are avalible Initialize Firebase
                    InitializeFirebase();
                }
                else
                {
                    Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
                }
            });
        

        warningLoginText.text = "";
        warningRegisterText.text = "";
        DontDestroyOnLoad(this.gameObject);
    }
    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void LoginButton()
    {
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    public void RegisterButton()
    {
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }
    public void Logout()
    {
        if (auth != null && User != null)
        {
            auth.SignOut();
        }
        SceneManager.LoadScene("AuthMenu");
    }

    private IEnumerator Login(string email, string password)
    {
        //Call the Firebase auth signin function passing the email and password
        Task<FirebaseUser> LoginTask = auth.SignInWithEmailAndPasswordAsync(email, password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            StartCoroutine(LoadUserData());
            _username = User.DisplayName;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            warningLoginText.text = "";
        }
    }

    private IEnumerator Register(string email, string password, string username)
    {
        if (username.Equals(""))
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            Task<FirebaseUser> RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    Task ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        AddUserToDatabase(username);
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                        warningRegisterText.text = "";
                    }
                }
            }
        }
    }

    private void AddUserToDatabase(string username)
    {
        StartCoroutine(UpdateUsername(username));
        StartCoroutine(UpdateScore(0));
    }

    private IEnumerator UpdateUsername(string username)
    {
        //Set the currently logged in user username in the database
        Task DBTask = DBReference.Child("users").Child(User.UserId).Child("username").SetValueAsync(username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
            _username = username;
        }
    }
    public IEnumerator UpdateScore(int score) //додати перевірку на макс результат
    {
        if (score >= _score)
        {
            //Set the currently logged in user deaths
            Task DBTask = DBReference.Child("users").Child(User.UserId).Child("score").SetValueAsync(score);

            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else
            {
                //Score is now updated
                _score = score;
            }
        }
    }

    private IEnumerator LoadUserData()
    {
        //Get the currently logged in user data
        Task<DataSnapshot> DBTask = DBReference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            _score = 0;
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            _score = int.Parse(snapshot.Child("score").Value.ToString());
        }
    }
    public IEnumerator LoadLeaderboardData(Action<Dictionary<string, int>> onComplete)
    {
        //Get all the users data ordered by kills amount
        Task<DataSnapshot> DBTask = DBReference.Child("users").OrderByChild("score").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            leaders.Clear();
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int score = int.Parse(childSnapshot.Child("score").Value.ToString());
                leaders.Add(username, score);
            }
            onComplete?.Invoke(leaders);
        }
    }
   
    public void GetUserHighscore(Action<Dictionary<string, int>> onComplete)
    {
        StartCoroutine(LoadLeaderboardData(onComplete));
    }
}
