using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject meteorPrefab; // Meteor nesnesinin prefab�
    public float spawnInterval = 5f; // Nesnenin olu�turulma aral���
    public float moveSpeed = 5f; // Nesnenin hareket h�z�

    private bool isSpawning = false; // Olu�turma durumu

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

            // Meteor nesnesi olu�tur
            GameObject newObject = Instantiate(meteorPrefab, GetRandomSpawnPosition(), Quaternion.identity);

            // Oyuncuya de�di�ini kontrol etmek i�in bir coroutine ba�lat
            StartCoroutine(CheckCollisionWithPlayer(newObject.transform));

            // Bir sonraki nesne olu�turma aral���n� bekle
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Ekran d���nda rastgele bir konum d�nd�r
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f);
        return spawnPosition;
    }

    private IEnumerator CheckCollisionWithPlayer(Transform objTransform)
    {
        while (true)
        {
            // Nesne hareket ettirme
            objTransform.position = Vector3.MoveTowards(objTransform.position, PlayerController.Instance.centerPoint.position, moveSpeed * Time.deltaTime);

            // Oyuncuya de�di mi kontrol et (Meteor tag'ine sahip olanlar)
            Collider2D[] colliders = Physics2D.OverlapCircleAll(objTransform.position, 0.5f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    // Oyuncuya de�di, game over
                    GameOverManager.Instance.GameOver();
                    yield break;
                }
            }

            yield return null;
        }
    }
}
