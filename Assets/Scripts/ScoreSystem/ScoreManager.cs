using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int value)
    {
        score += value;
        UpdateScoreText();
        CheckGameOver();
    }

    public void RemoveScore(int value)
    {
        score -= value;
        UpdateScoreText();
        CheckGameOver();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    private void CheckGameOver()
    {
        if(score < 0)
        {
            GameOverManager.Instance.GameOver();
        }
    }

    public int GetScore()
    {
        return score;
    }
}
