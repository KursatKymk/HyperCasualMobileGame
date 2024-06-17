using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static PlayerController Instance { get; private set; }

    public Transform centerPoint;
    [SerializeField] private float orbitRadius = 5f;
    [SerializeField] private float orbitSpeed = 50f;

    private float currentAngle = 0f;
    private bool isGameOver = false;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private bool moveRight = false;
    private bool moveLeft = false;

    private void Awake()
    {
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
            Shoot();
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.RightArrow) || moveRight)
        {
            currentAngle -= orbitSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || moveLeft)
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
        Vector3 directionToCenter = transform.position - centerPoint.position;
        float angle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // 90 derece çýkarmamýzýn sebebi, karakterin yukarýya bakmasýný saðlamak
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameObject.FindGameObjectWithTag("Bullet") == null)
            {
                Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Týklanan butona göre hareketi baþlat
        if (eventData.pointerEnter.CompareTag("RightButton"))
        {
            moveRight = true;
        }
        else if (eventData.pointerEnter.CompareTag("LeftButton"))
        {
            moveLeft = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Týklanan butona göre hareketi durdur
        if (eventData.pointerEnter.CompareTag("RightButton"))
        {
            moveRight = false;
        }
        else if (eventData.pointerEnter.CompareTag("LeftButton"))
        {
            moveLeft = false;
        }
    }

    public void SetGameOver()
    {
        isGameOver = true;
    }
}
