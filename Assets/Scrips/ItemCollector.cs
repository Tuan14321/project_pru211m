using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherry = 0;
    public GameObject itemPrefab;
    public GameObject pauseMenu;
    private int hightScore;
    private int score;

    private Camera mainCamera;
    [SerializeField] private Text cherryText;
    [SerializeField] private Text hightCherryText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text newHight;
    private void Start()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        mainCamera = Camera.main;
        hightScore = PlayerPrefs.GetInt("HightScore", 0);
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
    private void Update()
    {
        score = cherry;
        scoreText.text = "Cherries: " + score.ToString();
        if (score > hightScore)
        {
            newHight.text = "New Hight Score!!!";
            PlayerPrefs.SetInt("HightScore", score);
            hightCherryText.text = "Hight Cherries: " + score.ToString();
        }
        else
        {
            hightCherryText.text = "Hight Cherries: " + hightScore.ToString();
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
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void ReStart()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}


