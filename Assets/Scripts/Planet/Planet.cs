using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public static Planet Instance {  get; private set; }  

    [SerializeField] private float rotationSpeed = 50f;
    private bool reverseRotation = false;

    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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
}
