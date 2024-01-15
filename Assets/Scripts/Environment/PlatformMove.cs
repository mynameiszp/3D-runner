using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    private void FixedUpdate()
    {
        if (PlayerMove.Instance.PlayMode) transform.Translate(moveSpeed * Time.deltaTime * Vector3.back);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyTrigger"))
        {
            Destroy(gameObject);
        }
    }
    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
