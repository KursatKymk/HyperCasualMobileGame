using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public static TargetSpawner Instance; // Singleton instance

    public GameObject targetPrefab; // Olu�turulacak hedef prefab�
    public Transform centerPoint; // �emberin merkezi
    public float orbitRadius = 5f; // Y�r�nge yar��ap�

    private GameObject currentTarget; // Mevcut hedef referans�

    void Awake()
    {
        Instance = this; // Singleton instance'� atama
    }

    void Start()
    {
        SpawnTarget();
    }

    public void SpawnTarget()
    {
        // E�er mevcut hedef varsa, �nceki hedefi yok et
        if (currentTarget != null)
        {
            Destroy(currentTarget);
        }

        // Rastgele bir a�� se�
        float randomAngle = Random.Range(0f, 360f);
        float radians = randomAngle * Mathf.Deg2Rad;

        // Yeni pozisyonu hesapla
        float x = centerPoint.position.x + Mathf.Cos(radians) * orbitRadius;
        float y = centerPoint.position.y + Mathf.Sin(radians) * orbitRadius;
        Vector3 spawnPosition = new Vector3(x, y, 0f); // Z eksenini 0 yaparak 2D olmas�n� sa�l�yoruz

        // Karakterin merkezinin tam tersine bakmas�n� sa�la
        Vector3 directionFromCenter = spawnPosition - centerPoint.position;
        float angle = Mathf.Atan2(directionFromCenter.y, directionFromCenter.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // 90 derece ��karmam�z�n sebebi, karakterin yukar�ya bakmas�n� sa�lamak

        // Hedefi olu�tur ve mevcut hedef olarak ayarla
        currentTarget = Instantiate(targetPrefab, spawnPosition, rotation);
    }
}
