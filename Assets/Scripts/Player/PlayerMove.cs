using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private int desiredLine = 1;
    [SerializeField] private float laneDistance = 2;
    [SerializeField] private float movementSmoothness = 300;
    private bool isJumping;
    private bool isSliding;
    public bool PlayMode { get; private set; }
    private IControlStrategy _inputController;
    public static PlayerMove Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
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
        if (PlayMode)
        {
            SwipeHorizontally();
            SwipeVertically();
        }
    }

    private void StartGame()
    { 
        if (PlayMode == false && _inputController.WasTouched && _inputController.IsOverUI)
        {
            PlayMode = true;
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
        transform.position = Vector3.Lerp(transform.position, targetPosition, movementSmoothness * Time.deltaTime);
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

