using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public static Planet Instance {  get; private set; }  

    [SerializeField] private float rotationSpeed = 50f;
    private bool reverseRotation = false;

    private bool isGameOver = false;

    //New Mechanic That Shoots projectiles at the player.
    [SerializeField] private GameObject hostileLaser;
    [SerializeField] private GameObject player;
    private float spawnInterval = 5f;
    [SerializeField] private float laserSpeed = 5f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(ShootAtPlayerCoroutine());
    }

    private void Update()
    {
        if (!isGameOver)
        {
            float currentRotationSpeed = reverseRotation ? -rotationSpeed : rotationSpeed;
            transform.Rotate(Vector3.forward * currentRotationSpeed * Time.deltaTime);

            
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

        GameObject laserInstance = Instantiate(Laser, transform.position, Quaternion.identity);

        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        laserInstance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Rigidbody2D laserRB = laserInstance.GetComponent<Rigidbody2D>();

        if(laserRB != null)
        {
            laserRB.velocity = direction * laserSpeed * Time.deltaTime;
        }
        else
        {
            Debug.LogWarning("The Laser Does Not Have a RigidbodyComponent");
        }
    }
}
