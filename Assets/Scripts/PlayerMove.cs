using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private int desiredLine = 1;
    [SerializeField] private float laneDistance = 2;
    private Vector2 moveInput;
    private bool isJumping = false;
    private bool isSliding = false;
    private Animator animator;
    private void Start()
    {
        //characterController = GetComponent<CharacterController>();
        //#if UNITY_EDITOR
        //        _inputController = new KeyBoardInputManager();
        //#elif UNITY_ANDROID
        //            _inputController = new AndroidInputManager();
        //#endif
    }

    void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
        SwipeHorizontally();
        SwipeVertically();
    }
    private void SwipeHorizontally()
    {
        if (moveInput.y == 0)
        {
            if (moveInput.x == -1 && desiredLine < 2)
            {
                desiredLine++;
            }
            else if (moveInput.x == 1 && desiredLine > 0)
            {
                desiredLine--;
            }
            Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
            if (desiredLine == 2)
            {
                targetPosition += Vector3.left * laneDistance;
            }
            else if (desiredLine == 0)
            {
                targetPosition += Vector3.right * laneDistance;
            }
            transform.position = Vector3.Lerp(transform.position, targetPosition, 200 * Time.deltaTime);
        }
    }
    private void SwipeVertically()
    {
        if (moveInput.x == 0)
        {
            if (moveInput.y == 1)
            {
                if (!isJumping)
                {
                    isJumping = true;
                    StartCoroutine(PlayerAnimation.Instance.AnimateJump());
                    isJumping = false;
                }
                //collider?
            }
            else if (moveInput.y == -1)
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

    public Vector2 GetPlayerInput()
    {
        return moveInput;
    }
}

