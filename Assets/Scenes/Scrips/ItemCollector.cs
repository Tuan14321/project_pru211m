using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherry = 0;
    public GameObject itemPrefab;

    private Camera mainCamera;
    [SerializeField] private Text cherryText;
    private void Start()
    {
        mainCamera = Camera.main;
        SpawnRandomItem();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chery"))
        {
            Destroy(collision.gameObject);
            cherry++;
            cherryText.text = "Cherries: " + cherry;
            SpawnRandomItem();
        }
    }
    private void SpawnRandomItem()
    {
        Vector3 randomPosition;

        do
        {
            randomPosition = mainCamera.ViewportToWorldPoint(new Vector3(Random.value, Random.value, mainCamera.nearClipPlane));
            randomPosition.z = 0f;
        }
        while (CheckCollisionWithTilemap(randomPosition));

        Instantiate(itemPrefab, randomPosition, Quaternion.identity);
    }
    private bool CheckCollisionWithTilemap(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, itemPrefab.GetComponent<Collider2D>().bounds.size.x);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Map"))
            {
                return true;
            }
        }
        return false;
    }
}


