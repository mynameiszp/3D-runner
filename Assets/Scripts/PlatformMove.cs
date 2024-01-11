using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{

    public float moveSpeed = 10f;
    private void FixedUpdate()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.back);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DestroyTrigger"))
        {
            Destroy(gameObject);
        }
    }
}
