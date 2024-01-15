using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private bool isDead;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && isDead == false) ProcessDeath();
    }

    private void ProcessDeath()
    {
        isDead = true;
        StartCoroutine(GameOverManager.Instance.GameOver());
    }
    //private void Fade(float endValue, float duration, TweenCallback onEnd)
    //{
    //    if(fadeTween != null)
    //    {
    //        fadeTween.Kill(false);
    //    }
    //    fadeTween = canvasGroup.DOFade(endValue, duration);
    //    fadeTween.onComplete += onEnd;
    //}

    //private void FadeIn(float duration) {
    //    Fade(1f, duration, () =>
    //    {
    //        canvasGroup.interactable = true;
    //        canvasGroup.blocksRaycasts = true;
    //    });
    //}
    //private void FadeOut(float duration) {
    //    Fade(0f, duration, () =>
    //    {
    //        canvasGroup.interactable = false;
    //        canvasGroup.blocksRaycasts = false;
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    });
    //}
}
