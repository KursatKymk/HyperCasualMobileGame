using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    private bool reverseRotation = false;

    private void Update()
    {
        float currentRotationSpeed = reverseRotation ? -rotationSpeed : rotationSpeed;
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            reverseRotation = true;
        }
    }

    public void ReverseRotation()
    {
        reverseRotation = !reverseRotation;
    }
}
