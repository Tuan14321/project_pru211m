using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    private float timer = 0f;
    private bool isGameOver = false;
    [SerializeField] private Text timeText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
        timeText.text = "Time:" + timer;
    }
}
