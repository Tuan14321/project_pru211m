using UnityEngine;

public class DropSpeed : MonoBehaviour
{
    private float speedDrop = 4.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * speedDrop * Time.deltaTime;
    }
}
