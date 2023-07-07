using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator _animator;
    private BoxCollider2D boxCollider2D;
    [SerializeField] private GameObject deathPanel;

    private float timer = 0f;
    private bool isGameOver = false;
    [SerializeField] private Text timeText;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        Time.timeScale = 1;
        deathPanel.SetActive(false);
    }
    void Update()
    {
        CountTime();
    }
    private void CountTime()
    {
        if (!isGameOver)
        {
            timer += Time.deltaTime;

        }
        DisplayTime(timer);
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSeconds = (timeToDisplay % 1) * 1000;
        timeText.text = string.Format("Time: {0:00}:{1:00}:{2:00}", minutes, seconds, milliSeconds);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
            Time.timeScale = 0;
            deathPanel.SetActive(true);
        }

    }
    private void Die()
    {
        isGameOver = true;
        AudioManager.instance.Play("GameOver");
        _animator.SetTrigger("death");
        boxCollider2D.enabled = false;
        if (rb != null)
        {
            rb.gravityScale = 1f;
            rb.AddForce(Vector2.down * 5f, ForceMode2D.Impulse);
        }

    }
    public void ReStart()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
        deathPanel.SetActive(false);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
