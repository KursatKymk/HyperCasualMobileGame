using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject meteorPrefab; // Meteor nesnesinin prefabý
    public float spawnInterval = 5f; // Nesnenin oluþturulma aralýðý
    public float moveSpeed = 5f; // Nesnenin hareket hýzý

    private bool isSpawning = false; // Oluþturma durumu

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnObject());
        }
    }

    private IEnumerator SpawnObject()
    {
        while (isSpawning)
        {
            if (GameOverManager.Instance.isGameOver)
            {
                isSpawning = false;
                yield break;
            }

            // Meteor nesnesi oluþtur
            GameObject newObject = Instantiate(meteorPrefab, GetRandomSpawnPosition(), Quaternion.identity);

            // Oyuncuya deðdiðini kontrol etmek için bir coroutine baþlat
            StartCoroutine(CheckCollisionWithPlayer(newObject.transform));

            // Bir sonraki nesne oluþturma aralýðýný bekle
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Ekran dýþýnda rastgele bir konum döndür
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f);
        return spawnPosition;
    }

    private IEnumerator CheckCollisionWithPlayer(Transform objTransform)
    {
        while (true)
        {
            // Nesne hareket ettirme
            objTransform.position = Vector3.MoveTowards(objTransform.position, PlayerController.Instance.centerPoint.position, moveSpeed * Time.deltaTime);

            // Oyuncuya deðdi mi kontrol et (Meteor tag'ine sahip olanlar)
            Collider2D[] colliders = Physics2D.OverlapCircleAll(objTransform.position, 0.5f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    // Oyuncuya deðdi, game over
                    GameOverManager.Instance.GameOver();
                    yield break;
                }
            }

            yield return null;
        }
    }
}
