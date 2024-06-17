using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    public GameObject background; // Background GameObject referansý

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

        // Game Over olduðunda Background GameObject'ini aktifleþtir
        if (background != null)
        {
            background.SetActive(true);
        }

        // PlayerController.Instance üzerinden SetGameOver metodunu çaðýr
        if (PlayerController.Instance != null)
        {
            PlayerController.Instance.SetGameOver();
        }
    }
}
