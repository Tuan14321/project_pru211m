using UnityEngine;

public class Ramdon_Canon : MonoBehaviour
{
    public GameObject objectPrefab; // Prefab của game object cần xuất hiện
    public Vector3 spawnPosition; // Vị trí xuất hiện của game object

    public float minSpawnDelay = 1f; // Khoảng thời gian tối thiểu giữa các lần xuất hiện
    public float maxSpawnDelay = 3f; // Khoảng thời gian tối đa giữa các lần xuất hiện

    public float objectLifetime = 20f; // Thời gian tồn tại của game object

    private float nextSpawnTime; // Thời điểm xuất hiện tiếp theo

    private void Start()
    {
        // Tính thời điểm xuất hiện tiếp theo ban đầu
        nextSpawnTime = Time.time + GetRandomSpawnDelay();
    }

    private void Update()
    {
        // Kiểm tra nếu đã đến thời điểm xuất hiện
        if (Time.time >= nextSpawnTime)
        {
            // Tạo game object mới tại vị trí đã chỉ định
            GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);

            // Hủy game object sau thời gian objectLifetime
            Destroy(newObject, objectLifetime);

            // Tính thời điểm xuất hiện tiếp theo
            nextSpawnTime = Time.time + GetRandomSpawnDelay();
        }
    }

    private float GetRandomSpawnDelay()
    {
        // Lấy một khoảng thời gian ngẫu nhiên trong khoảng minSpawnDelay đến maxSpawnDelay
        return Random.Range(minSpawnDelay, maxSpawnDelay);
    }
}
