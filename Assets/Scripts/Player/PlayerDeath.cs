using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            ProcessDeath();
            ObjectsPool.Instance.DeactivateAll();
        }
    }

    private void ProcessDeath()
    {
        StartCoroutine(GameOverManager.Instance.GameOver());
    }
}
