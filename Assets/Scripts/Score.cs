using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private readonly string _initialText = "Score: ";
    private int _score;
    private float _initialSpeed;
    private float _scoreIncrease = 0.0001f;
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
        _initialSpeed = ObstacleMovement.GetMoveSpeed();
    }
    private void Update()
    {
        if (PlayerController.Instance.PlayMode)
        {
            _score = (int)(_initialSpeed * 100);
            scoreText.text = _initialText + _score;
            if(ObstacleSpawner.Instance.GetPassedFirstObject()) _initialSpeed += _scoreIncrease;
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
