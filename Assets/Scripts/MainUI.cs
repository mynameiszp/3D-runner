using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas leaderboardCanvas;
    [SerializeField] private Canvas mainCanvas;

    private void Awake()
    {
        leaderboardCanvas.enabled = false;
        //mainCanvas.enabled = true;
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
