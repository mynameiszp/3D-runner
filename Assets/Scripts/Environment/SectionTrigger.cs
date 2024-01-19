using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    [SerializeField] private GameObject road;

    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.CompareTag("CreateTrigger"))
        {
            Instantiate(road, new Vector3(0, 0, 105), Quaternion.identity);
        }
    }       
}
