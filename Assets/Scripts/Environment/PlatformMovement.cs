using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    private void FixedUpdate()
    {
        if (PlayerMovement.Instance.PlayMode) transform.Translate(moveSpeed * Time.deltaTime * Vector3.back);
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
