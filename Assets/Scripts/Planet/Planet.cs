using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public static Planet Instance { get; private set; }

    [SerializeField] private float rotationSpeed = 50f;
    private bool reverseRotation = false;

    private bool isGameOver = false;
    private int gameScore;

    // New Mechanic That Shoots projectiles at the player.
    [SerializeField] private GameObject hostileLaser;
    [SerializeField] private GameObject player;
    private float spawnInterval = 5f;
    [SerializeField] private float laserSpeed = 5f;

    private Coroutine shootingCoroutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        // Initially, do not start shooting coroutine
    }

    private void Update()
    {
        gameScore = ScoreManager.Instance.GetScore(); // Gets the score from the score manager.

        if (!isGameOver && gameScore < 10)
        {
            float currentRotationSpeed = reverseRotation ? -rotationSpeed : rotationSpeed;
            transform.Rotate(Vector3.forward * currentRotationSpeed * Time.deltaTime);
        }
        else if (!isGameOver && gameScore >= 10)
        {
            rotationSpeed = 100f;
            float currentRotationSpeed = reverseRotation ? -rotationSpeed : rotationSpeed;
            transform.Rotate(Vector3.forward * currentRotationSpeed * Time.deltaTime);
        }

        if (!isGameOver && gameScore > 5 && shootingCoroutine == null)
        {
            shootingCoroutine = StartCoroutine(ShootAtPlayerCoroutine());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            ScoreManager.Instance.RemoveScore(1);
            reverseRotation = true;
        }
    }

    public void ReverseRotation()
    {
        reverseRotation = !reverseRotation;
    }

    public void SetGameOverPlanet()
    {
        isGameOver = true;

        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
        }
    }

    private IEnumerator ShootAtPlayerCoroutine()
    {
        while (!isGameOver)
        {
            ShootAtPlayer(hostileLaser, player);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void ShootAtPlayer(GameObject Laser, GameObject Player)
    {
        if (Player == null)
        {
            Debug.LogWarning("Player Object is not Assigned");
            return;
        }

        Vector3 playerPosition = Player.transform.position;
        Vector3 direction = (playerPosition - transform.position).normalized;

        // Calculate the angle towards the player and adjust for 90-degree rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

        GameObject laserInstance = Instantiate(Laser, transform.position, Quaternion.Euler(0, 0, angle));

        Rigidbody2D laserRB = laserInstance.GetComponent<Rigidbody2D>();

        if (laserRB != null)
        {
            laserRB.velocity = direction * laserSpeed;
        }
        else
        {
            Debug.LogWarning("The Laser Does Not Have a RigidbodyComponent");
        }
    }
}
