using System.Collections;
using UnityEngine;

public class DropArrow : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] float secondDrop = 4f;
    [SerializeField] float minTrans;
    [SerializeField] float maxTrans;
    void Start()
    {
        StartCoroutine(DropArrows());
    }

    IEnumerator DropArrows()
    {
        while (true)
        {
            var wanted = Random.Range(minTrans, maxTrans);
            var position = new Vector3(wanted, transform.position.y);
            GameObject newArrow = Instantiate(arrow, position, Quaternion.identity);

            yield return new WaitForSeconds(secondDrop);
            Destroy(newArrow, 5f);
        }
    }
}
