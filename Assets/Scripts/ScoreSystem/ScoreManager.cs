using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endScoreText;
    private int score = 0;
    private int highScore = 0;

    public GameObject objectSpawner;

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
        highScore = value;
        UpdateScoreText();
        CheckGameOver();

        if(score > 5 && objectSpawner != null)
        {
            objectSpawner.GetComponent<ObjectSpawner>().StartSpawning();
        }
    }

    public void RemoveScore(int value)
    {
        score -= value;
        UpdateScoreText();
        CheckGameOver();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        highScore = score;
        endScoreText.text = "Score: " + highScore.ToString();
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

    public int GetHighScore()
    {
        return highScore;
    }
}
