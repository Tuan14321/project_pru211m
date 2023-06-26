using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherry = 0;

    [SerializeField] private Text cherryText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chery"))
        {
            Destroy(collision.gameObject);
            cherry++;
            cherryText.text = "Cherries: " + cherry;
        }
    }
}
