using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Mermi hýzý

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime); // Mermiyi yukarý doðru hareket ettir
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // Mermi görünmez olduðunda yok et
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            Destroy(gameObject); // Mermiyi yok et
            Destroy(other.gameObject); // Hedefi yok et
            // Yeni hedef oluþturma iþlemleri buraya eklenebilir
            // Örneðin, Target script'indeki SpawnNewTarget fonksiyonu çaðrýlabilir
        }
    }
}
