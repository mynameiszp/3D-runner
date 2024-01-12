using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardInputManager : IControlStrategy
{
    public bool Left { get; private set; } = false; 
    public bool Right { get; private set; } = false;
    public bool Up { get; private set; } = false;
    public bool Down { get; private set; } = false;

    public void ManageInput()
    {
        Left = Right = Up = Down = false;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) Left = true;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) Right = true;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) Up = true;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) Down = true;
    }
}
