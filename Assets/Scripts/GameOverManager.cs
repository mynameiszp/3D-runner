using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private Canvas deathCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject watchAdButton;
    private Tween fadeTween;
    private Score scoreManager;
    private bool hasWatchedAd;
    public static GameOverManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        deathCanvas.enabled = false;
        scoreManager = Score.Instance;
    }

    public IEnumerator GameOver()
    {
        PlayerController.Instance.PlayMode = false;
        gameCanvas.SetActive(false);
        PlayerAnimation.Instance.Die();
        StartCoroutine(FirebaseManager.Instance.UpdateScore(scoreManager.GetScore()));
        deathCanvas.enabled = true;
        restartButton.SetActive(false);
        watchAdButton.SetActive(false);
        BlinkOut(0f, text);
        yield return StartCoroutine(AnimateImage(2f));
        yield return StartCoroutine(AnimateText(2, 1f));
        PlayerAnimation.Instance.Revive();
        restartButton.SetActive(true);
        if (hasWatchedAd == false) watchAdButton.SetActive(true);
    }

    private void BlinkIn(float duration, Graphic graphics)
    {
        fadeTween = graphics.DOFade(1f, duration);
    }
    private void BlinkOut(float duration, Graphic graphics)
    {
        fadeTween = graphics.DOFade(0f, duration);
    }

    private IEnumerator AnimateText(int repeat, float duration)
    { 
        for (int i = 0; i < repeat; i++)
        {            
            BlinkIn(duration, text);
            yield return new WaitForSecondsRealtime(duration);
            BlinkOut(duration, text);
            yield return new WaitForSecondsRealtime(duration);
        }
        BlinkIn(duration, text);
        yield return new WaitForSecondsRealtime(duration);
    }
    private IEnumerator AnimateImage(float duration)
    {
        BlinkOut(0f, image);
        BlinkIn(duration, image);
        yield return new WaitForSecondsRealtime(duration);
    }

    public void Revive()
    {
        //PlayerAnimation.Instance.Revive();
        hasWatchedAd = true;
        deathCanvas.enabled = false;
        gameCanvas.SetActive(true);
        PlayerController.Instance.PlayMode = true;
        ObstacleSpawner.Instance.HasRestartedGame = true;
    }
}
