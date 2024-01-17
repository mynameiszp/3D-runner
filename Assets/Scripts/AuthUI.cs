using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthUI : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas logInCanvas;
    [SerializeField] private Canvas signUpCanvas;

    public static AuthUI instance;

    private void Awake()
    {
        signUpCanvas.enabled = false;
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void OnSignUp()
    {
        logInCanvas.enabled = false;
        signUpCanvas.enabled = true;
    }

    public void OnBack()
    {
        logInCanvas.enabled = true;
        signUpCanvas.enabled = false;
    }
}
