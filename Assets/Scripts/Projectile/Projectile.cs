using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Mermi h�z�

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime); // Mermiyi yukar� do�ru hareket ettir
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // Mermi g�r�nmez oldu�unda yok et
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            Destroy(gameObject); // Mermiyi yok et
            Destroy(other.gameObject); // Hedefi yok et
        }
    }
}
