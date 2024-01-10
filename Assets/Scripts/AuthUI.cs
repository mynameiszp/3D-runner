using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthUI : MonoBehaviour
{
    [Header("Canvas")]
    public Canvas logInCanvas;
    public Canvas signUpCanvas;

    private void Awake()
    {
        signUpCanvas.enabled = false;
    }

    public void OnSignUp()
    {
        logInCanvas.enabled = false;
        signUpCanvas.enabled = true;
        //wait until button is pressed
    }

    public void OnLogIn()
    {
        //call AuthManager method
    }

}
