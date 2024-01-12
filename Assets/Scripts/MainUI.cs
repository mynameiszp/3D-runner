using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    [Header("Canvas")]
    public Canvas leaderboardCanvas;
    public Canvas mainCanvas;

    private void Awake()
    {
        leaderboardCanvas.enabled = false;
    }
    private void Update()
    {
        if (PlayerMove.Instance.PlayMode) mainCanvas.enabled = false;
    }
    public void OnExit()
    {
        Application.Quit();
    }

    public void OnOpenLeaderboard()
    {
        leaderboardCanvas.enabled = true;
    }    
    
    public void OnCloseLeaderboard()
    {
        leaderboardCanvas.enabled = false;
    }    
}
