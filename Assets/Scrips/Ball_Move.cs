using UnityEngine;

public class Ball_Move : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab của viên đạn
    public Transform spawnPoint; // Điểm xuất hiện của viên đạn

    public void StartShootAnimation()
    {
        // Chạy animation bắn đạn
        GetComponent<Animator>().Play("ShootAnimation");
    }

    // Gọi từ event animation
    public void SpawnBullet()
    {
        // Tạo viên đạn từ prefab tại điểm spawnPoint
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);

        // Thiết lập hướng bay cho viên đạn (đến điểm đối diện bên kia)
        Vector3 targetPosition = GetOppositePoint(spawnPoint.position);
        Vector3 direction = targetPosition - spawnPoint.position;
        bullet.GetComponent<Rigidbody>().velocity = direction.normalized * 10f; // Tốc độ bay của viên đạn (10f ở đây là ví dụ)

        // Hủy viên đạn sau một khoảng thời gian (nếu cần)
        Destroy(bullet, 5f); // Ví dụ: Hủy viên đạn sau 5 giây
    }

    private Vector3 GetOppositePoint(Vector3 originalPoint)
    {
        // Tính điểm đối diện bên kia (ví dụ: trục X đổi dấu)
        Vector3 oppositePoint = originalPoint;
        oppositePoint.x = -originalPoint.x;
        return oppositePoint;
    }
}
