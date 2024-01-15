using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private string _initialText = "Score: ";
    private int _score;
    public static Score Instance;
    private bool countStarted;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        scoreText.text = "";
    }
    private void Start()
    {        
        _score = 0;
    }
    private void Update()
    {
        if(!countStarted && ObstacleMove.GetMoveSpeed() == 1)
        {
            countStarted = true;
        }
        if (countStarted)
        {
            _score = (int)(ObstacleMove.GetMoveSpeed() * 100);
            scoreText.text = _initialText + _score;
        }
    }
    public void ResetScore()
    {
        _score = 0;
    }

    public int GetScore()
    {
        return _score;
    }
}
