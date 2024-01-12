using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 1f;
    private ObjectsPool objectsPool;
    private void Start()
    {
        objectsPool = ObjectsPool.Instance;
    }
    private void FixedUpdate()
    {
        if (PlayerMove.Instance.PlayMode)
        {
            foreach (GameObject gameObject in objectsPool.GetPooledObjects())
            {
                if (gameObject.activeInHierarchy)
                    gameObject.transform.Translate(moveSpeed * Time.deltaTime * Vector3.back);
            }
        }
    }
}
