using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    [Header("Canvas")]
    public Canvas leaderboardCanvas;

    private void Awake()
    {
        leaderboardCanvas.enabled = false;
    }
    public void OnExit()
    {
        Application.Quit();
    }

    public void OnLogOut()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
        //call AuthManager method
    }

    public void OpenLeaderboard()
    {
        leaderboardCanvas.enabled = true;
    }    
    
    public void CloseLeaderboard()
    {
        leaderboardCanvas.enabled = false;
    }
}
