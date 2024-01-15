using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    private CanvasGroup deathCanvas;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject restartButton;
    private Tween fadeTween;
    public static GameOverManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        deathCanvas = GetComponent<CanvasGroup>();
        deathCanvas.alpha = 0f;
    }
    public void OnRestart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator GameOver()
    {
        deathCanvas.alpha = 1f;
        restartButton.SetActive(false);
        yield return StartCoroutine(AnimateText(2, 1f));
        Time.timeScale = 0;
        restartButton.SetActive(true);
    }

    private void BlinkIn(float duration)
    {
        fadeTween = text.DOFade(1f, duration);
    }
    private void BlinkOut(float duration)
    {
        fadeTween = text.DOFade(0f, duration);
    }

    private IEnumerator AnimateText(int repeat, float duration)
    {
        for (int i = 0; i < repeat; i++)
        {
            BlinkOut(duration);
            yield return new WaitForSecondsRealtime(duration);
            BlinkIn(duration);
            yield return new WaitForSecondsRealtime(duration);
        }
    }
}
