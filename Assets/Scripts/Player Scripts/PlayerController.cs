using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    PlayerControls controls;
    private float direction = 0;

    public Transform centerPoint;
    [SerializeField] private float orbitRadius = 5f;
    [SerializeField] private float orbitSpeed = 50f;

    private float currentAngle = 0f;
    private bool isGameOver = false;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Enable();

        controls.Controls.Move.performed += ctx =>
        {
            direction = ctx.ReadValue<float>();
        };

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!isGameOver)
        {
            Movement();
            controls.Controls.Fire.performed += ctx =>
            {
                Shoot();
            };
        }
    }

    private void Movement()
    {
        currentAngle += direction * orbitSpeed * Time.deltaTime;

        float radians = currentAngle * Mathf.Deg2Rad;

        float x = centerPoint.position.x + Mathf.Cos(radians) * orbitRadius;
        float y = centerPoint.position.y + Mathf.Sin(radians) * orbitRadius;

        // Karakterin pozisyonunu güncelle
        Vector3 newPosition = new Vector3(x, y, transform.position.z);
        transform.position = newPosition;

        // Karakterin merkezine bakmasýný saðla
        Vector3 directionToCenter = transform.position - centerPoint.position;
        float angle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // 90 derece çýkarmamýzýn sebebi, karakterin yukarýya bakmasýný saðlamak
    }

    private void Shoot()
    {
        if (GameObject.FindGameObjectWithTag("Bullet") == null)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }

    public void SetGameOver()
    {
        isGameOver = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Meteor"))
        {
            SetGameOver();
            Debug.Log("asdasdas");
        }
    }
}
