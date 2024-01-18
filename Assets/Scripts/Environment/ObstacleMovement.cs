using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private static float moveSpeed = 0.5f;
    [SerializeField] private float speedIncrease = 0.00001f;
    private ObjectsPool objectsPool;
    private float speedLimit = 3f;
    private static float initialMoveSpeed;
    private void Start()
    {
        initialMoveSpeed = moveSpeed;
        objectsPool = ObjectsPool.Instance;
    }
    private void FixedUpdate()
    {
        if (PlayerController.Instance.PlayMode)
        {
            foreach (GameObject gameObject in objectsPool.GetPooledObjects())
            {
                if (gameObject.activeInHierarchy)
                    gameObject.transform.Translate(moveSpeed * Time.deltaTime * Vector3.back);
            }
            if(moveSpeed < speedLimit) moveSpeed += speedIncrease;
        }
    }
    public static float GetMoveSpeed()
    {
        return moveSpeed;
    }    
    public static void ResetMoveSpeed()
    {
        moveSpeed = initialMoveSpeed;
    }
}
