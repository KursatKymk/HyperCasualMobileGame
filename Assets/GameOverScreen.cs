using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI pointText;

    public void Setup(int score)
    {
        gameObject.SetActive(true);
    }
}
