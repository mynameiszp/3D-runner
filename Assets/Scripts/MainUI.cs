using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas leaderboardCanvas;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas gameCanvas;
    [Header("Leaderboard")]
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private Transform table;
    private bool isPaused;

    private void Awake()
    {
        leaderboardCanvas.enabled = false;
        gameCanvas.enabled = false;
        //mainCanvas.enabled = true;
    }
    private void Update()
    {
        if (PlayerMovement.Instance.PlayMode)
        {
            mainCanvas.enabled = false;
            gameCanvas.enabled = true;
        }
    }
    public void OnExit()
    {
        Application.Quit();
    }

    public void OnOpenLeaderboard()
    {
        leaderboardCanvas.enabled = true;
        Leaderboard.Instance.ShowLeaderboard(table, rowPrefab);
    }    

    public void OnCloseLeaderboard()
    {
        leaderboardCanvas.enabled = false;
    }
    public void OnRestart()
    {
        Time.timeScale = 1;
        Score.Instance.ResetScore();
        ObstacleMovement.ResetMoveSpeed();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnPause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }
}
