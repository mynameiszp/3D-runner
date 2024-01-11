using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private int desiredLine = 1;
    [SerializeField] private float laneDistance = 2;
    private Vector2 moveInput;
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
        Swipe();
    }
    private void Swipe()
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

