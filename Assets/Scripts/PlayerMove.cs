using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[System.Serializable]
public enum SIDE { Left, Mid, Right};
public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 10;
    //public float swipeSpeed = 50;
    //private IControlStrategy _inputController;

    //bool swipeLeft;
    //bool swipeRight;
    //bool swipeUp;
    //bool swipeDown;
    //public SIDE m_side = SIDE.Mid;
    //float xPos = 0f;
    //[SerializeField] float xValue = 3f;
    //CharacterController characterController;

    private void Start()
    {
        //characterController = GetComponent<CharacterController>();
//#if UNITY_EDITOR
//        _inputController = new KeyBoardInputManager();
//#elif UNITY_ANDROID
//            _inputController = new AndroidInputManager();
//#endif
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
        //if (_inputController != null) _inputController.Move(this.transform); ;

    //    swipeLeft = Input.GetKeyDown(KeyCode.LeftArrow);
    //    swipeRight = Input.GetKeyDown(KeyCode.RightArrow);
    //    if (swipeLeft)
    //    {
    //        if(m_side == SIDE.Mid)
    //        {
    //            xPos = -xValue;
    //            m_side = SIDE.Left;
    //        } else if(m_side == SIDE.Right){
    //            xPos = 0f;
    //            m_side = SIDE.Mid;
    //        }
    //    }
    //    if (swipeRight)
    //    {
    //        if(m_side == SIDE.Mid)
    //        {
    //            xPos = xValue;
    //            m_side = SIDE.Right;
    //        }
    //        else if(m_side == SIDE.Left){
    //            xPos = 0f;
    //            m_side = SIDE.Mid;
    //        }
    //    }
    //    characterController.Move((xPos - transform.position.x) * Vector3.right);
    }

    //void OnMove(InputValue input)
    //{
        
    //}
}

