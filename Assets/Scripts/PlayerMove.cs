using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private int desiredLine = 1;
    [SerializeField] private float laneDistance = 2;
    private bool isJumping;
    private bool isSliding;
    private bool playMode;
    private IControlStrategy _inputController;
    private void Start()
    {
        #if UNITY_EDITOR
        _inputController = new KeyBoardInputManager();
        #elif UNITY_ANDROID
        _inputController = new AndroidInputManager();
        #endif
    }
    private void Update()
    {
        _inputController.ManageInput();
        StartGame();
        if (playMode)
        {
            SwipeHorizontally();
            SwipeVertically();
        }
    }

    private void StartGame()
    {
        if (playMode == false &&_inputController.WasTouched)
        {
            playMode = true;
            PlayerAnimation.Instance.AnimateRun();
        }
    }

    private void SwipeHorizontally()
    {
        if (_inputController.Right && desiredLine < 2)
        {
            desiredLine++;
        }
        else if (_inputController.Left && desiredLine > 0)
        {
            desiredLine--;
        }
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLine == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        else if (desiredLine == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, 200 * Time.deltaTime);
    }
    private void SwipeVertically()
    {
        if (_inputController.Up)
        {
            if (!isJumping)
            {
                isJumping = true;
                StartCoroutine(PlayerAnimation.Instance.AnimateJump());
                isJumping = false;
            }
            //collider?
        }
        else if (_inputController.Down)
        {
            if (!isSliding)
            {
                isSliding = true;
                StartCoroutine(PlayerAnimation.Instance.AnimateSlide());
                isSliding = false;
            }
            //collider?
        }
    }
}

