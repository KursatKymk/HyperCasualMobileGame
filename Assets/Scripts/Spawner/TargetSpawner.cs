using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public static TargetSpawner Instance; // Singleton instance

    public GameObject targetPrefab; // Oluþturulacak hedef prefabý
    public Transform centerPoint; // Çemberin merkezi
    public float orbitRadius = 5f; // Yörünge yarýçapý

    private GameObject currentTarget; // Mevcut hedef referansý

    void Awake()
    {
        Instance = this; // Singleton instance'ý atama
    }

    void Start()
    {
        SpawnTarget();
    }

    public void SpawnTarget()
    {
        // Eðer mevcut hedef varsa, önceki hedefi yok et
        if (currentTarget != null)
        {
            Destroy(currentTarget);
        }

        // Rastgele bir açý seç
        float randomAngle = Random.Range(0f, 360f);
        float radians = randomAngle * Mathf.Deg2Rad;

        // Yeni pozisyonu hesapla
        float x = centerPoint.position.x + Mathf.Cos(radians) * orbitRadius;
        float y = centerPoint.position.y + Mathf.Sin(radians) * orbitRadius;
        Vector3 spawnPosition = new Vector3(x, y, 0f); // Z eksenini 0 yaparak 2D olmasýný saðlýyoruz

        // Karakterin merkezinin tam tersine bakmasýný saðla
        Vector3 directionFromCenter = spawnPosition - centerPoint.position;
        float angle = Mathf.Atan2(directionFromCenter.y, directionFromCenter.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // 90 derece çýkarmamýzýn sebebi, karakterin yukarýya bakmasýný saðlamak

        // Hedefi oluþtur ve mevcut hedef olarak ayarla
        currentTarget = Instantiate(targetPrefab, spawnPosition, rotation);
    }
}
