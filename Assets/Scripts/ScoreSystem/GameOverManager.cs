using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance {  get; private set; }

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

        if(PlayerController.Instance != null)
        {
            PlayerController.Instance.SetGameOver();
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
