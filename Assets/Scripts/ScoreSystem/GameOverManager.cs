using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    public GameObject background; // Background GameObject referans�

    public bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void GameOver()
    {
        Debug.Log("Game Over!!");

        isGameOver = true;

        // Game Over oldu�unda Background GameObject'ini aktifle�tir
        if (background != null)
        {
            background.SetActive(true);
            PlayerController.Instance.SetGameOver();
            Planet.Instance.SetGameOverPlanet();
        }

        // PlayerController.Instance �zerinden SetGameOver metodunu �a��r
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.SetGameOver();
        }
    }
}
