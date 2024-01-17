using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private readonly string _initialText = "Score: ";
    private int _score;
    private bool countStarted;
    public static Score Instance;
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
        _score = (int)(ObstacleMovement.GetMoveSpeed() * 100);
        scoreText.text = _initialText + _score;
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
