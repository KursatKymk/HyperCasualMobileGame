using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            // Yeni hedefi oluþtur
            TargetSpawner.Instance.SpawnTarget();
        }
    }
}
