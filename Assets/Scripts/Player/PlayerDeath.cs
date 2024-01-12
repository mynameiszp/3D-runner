using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle")) ProcessDeath();
    }

    private void ProcessDeath()
    {
        Debug.Log("Dead");
        //Time.timeScale = 0;
    }
}
