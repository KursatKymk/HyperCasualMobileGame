using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform centerPoint;
    [SerializeField] private float orbitRadius = 5f;
    [SerializeField] private float orbitSpeed = 50f;

    private float currentAngle = 0f;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    void Update()
    {
        Movement();
        Shoot();    
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentAngle -= orbitSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentAngle += orbitSpeed * Time.deltaTime;
        }

        float radians = currentAngle * Mathf.Deg2Rad;

        float x = centerPoint.position.x + Mathf.Cos(radians) * orbitRadius;
        float y = centerPoint.position.y + Mathf.Sin(radians) * orbitRadius;

        // Karakterin pozisyonunu güncelle
        Vector3 newPosition = new Vector3(x, y, transform.position.z);
        transform.position = newPosition;

        // Karakterin merkezine bakmasýný saðla
        Vector3 directionToCenter = centerPoint.position - transform.position;
        float angle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // 90 derece çýkarmamýzýn sebebi, karakterin yukarýya bakmasýný saðlamak
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab,bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }
}
