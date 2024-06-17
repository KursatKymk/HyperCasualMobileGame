using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Mermiyi yok et
            Destroy(other.gameObject);

            // Hedefi yok et
            Destroy(gameObject);

            //score artt�r
            ScoreManager.Instance.AddScore(1);

            Planet planet = FindObjectOfType<Planet>();
            if( planet != null)
            {
                planet.ReverseRotation();
            }

            // Yeni hedefi olu�tur
            TargetSpawner.Instance.SpawnTarget();
        }
    }
}
