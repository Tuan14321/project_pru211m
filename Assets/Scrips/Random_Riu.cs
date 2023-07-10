using System.Collections.Generic;
using UnityEngine;

public class Random_Riu : MonoBehaviour
{
    public GameObject[] prefabs; // Mảng chứa các prefabs
    public Transform[] positions; // Mảng chứa các vị trí cố định

    public float fadeInDuration = 2f; // Thời gian nháy nháy trước khi hiển thị prefab hoàn toàn
    public float spawnDelay = 15f; // Thời gian chờ giữa các lần xuất hiện của prefab
    public float nextSpawnDelay = 5f; // Thời gian chờ trước khi hiển thị prefab tiếp theo
    private List<Transform> availablePositions; // Danh sách các vị trí có thể random vào

    private void Start()
    {
        availablePositions = new List<Transform>(positions);
        StartCoroutine(SpawnPrefabsRepeatedly());
    }

    private System.Collections.IEnumerator SpawnPrefabsRepeatedly()
    {
        while (true)
        {
            // Lấy prefab và vị trí spawn ngẫu nhiên từ danh sách các vị trí có thể random vào
            int prefabIndex = Random.Range(0, prefabs.Length);
            GameObject prefab = prefabs[prefabIndex];
            Transform spawnPosition = GetRandomAvailablePosition();

            // Nháy nháy prefab trong khoảng thời gian fadeInDuration
            StartCoroutine(FadeInPrefab(prefab, spawnPosition));

            yield return new WaitForSeconds(spawnDelay);

            // Đợi một khoảng thời gian trước khi hiển thị prefab tiếp theo
            yield return new WaitForSeconds(nextSpawnDelay);
        }
    }

    private Transform GetRandomAvailablePosition()
    {
        int randomIndex = Random.Range(0, availablePositions.Count);
        Transform position = availablePositions[randomIndex];
        availablePositions.RemoveAt(randomIndex);
        return position;
    }

    private System.Collections.IEnumerator FadeInPrefab(GameObject prefab, Transform spawnPosition)
    {
        // Instantiate prefab và tắt rendering ban đầu
        GameObject instance = Instantiate(prefab, spawnPosition.position, Quaternion.identity);
        Renderer[] renderers = instance.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }

        // Nháy nháy prefab trong khoảng thời gian fadeInDuration
        float t = 0f;
        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / fadeInDuration);
            foreach (Renderer renderer in renderers)
            {
                renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, alpha);
            }
            yield return null;
        }

        // Hiển thị prefab hoàn toàn
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = true;
        }
    }
}
