using System.Collections;
using UnityEngine;

public class RandomCherry : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject itemPrefab;
    public GameObject titleMap;
    private bool spawningEnabled = true;
    private void Start()
    {
        StartCoroutine(SpawnItems());
    }

    public IEnumerator SpawnItems()
    {
        while (spawningEnabled)
        {
            yield return new WaitForSeconds(1f); // Thời gian chờ giữa các lần spawn

            Vector2 spawnPosition = GetRandomSpawnPosition();

            GameObject newItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
            newItem.AddComponent<ItemCollector>();
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        Vector2 spawnPosition = Vector2.zero;
        bool validSpawn = false;
        int maxAttempts = 100; // Số lần tối đa để tránh vòng lặp vô hạn

        while (!validSpawn && maxAttempts > 0)
        {
            spawnPosition = GetRandomPositionWithinViewport();

            Collider2D[] colliders = Physics2D.OverlapPointAll(spawnPosition);
            bool collidesWithTitleMap = false;

            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject == titleMap)
                {
                    collidesWithTitleMap = true;
                    break;
                }
            }

            if (!collidesWithTitleMap)
            {
                validSpawn = true;
            }

            maxAttempts--;
        }

        if (maxAttempts == 0)
        {
            Debug.LogWarning("Cannot find valid spawn position for the item.");
        }

        return spawnPosition;
    }
    public void StopSpawning()
    {
        spawningEnabled = false;
    }

    private Vector2 GetRandomPositionWithinViewport()
    {
        Vector2 randomPosition = Vector2.zero;
        Camera mainCamera = Camera.main;

        float randomX = Random.Range(0f, 1f);
        float randomY = Random.Range(0f, 1f);

        randomPosition = mainCamera.ViewportToWorldPoint(new Vector2(randomX, randomY));

        return randomPosition;
    }
}
