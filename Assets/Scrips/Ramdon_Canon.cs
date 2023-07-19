using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramdon_Canon : MonoBehaviour
{
    public Transform startPosition;

    public Ramdon_Canon(Transform startPosition)
    {
        this.startPosition = startPosition;
    }

    public Transform endPosition;
    public float moveSpeed = 2f; // Tốc độ di chuyển của đối tượng
    public float delayMin = 4f; // Thời gian chờ tối thiểu trước khi di chuyển
    public float delayMax = 5f; // Thời gian chờ tối đa trước khi di chuyển
    public int objectPoolSize = 5; // Số lượng đối tượng trong pool
    public GameObject objectPrefab; // Prefab của đối tượng cần tạo lại

    private List<GameObject> objectPool = new List<GameObject>();
    private bool isMoving = false;

    private void Start()
    {
        CreateObjectPool();

        StartCoroutine(StartMovementAfterDelay());
    }

    private void CreateObjectPool()
    {
        for (int i = 0; i < objectPoolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab, startPosition.position, Quaternion.identity);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }

    private IEnumerator StartMovementAfterDelay()
    {
        while (true)
        {
            float delay = Random.Range(delayMin, delayMax);
            yield return new WaitForSeconds(delay);

            GameObject obj = GetObjectFromPool();
            obj.SetActive(true);
            StartCoroutine(MoveObject(obj));

            yield return new WaitForSeconds(delay);
        }
    }

    private GameObject GetObjectFromPool()
    {
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }

        // Nếu không còn đối tượng không hoạt động trong pool, tạo một đối tượng mới
        GameObject newObj = Instantiate(objectPrefab, startPosition.position, Quaternion.identity);
        objectPool.Add(newObj);
        return newObj;
    }

    private IEnumerator MoveObject(GameObject obj)
    {
        isMoving = true;

        Transform objTransform = obj.transform;

        while (objTransform.position != endPosition.position)
        {
            objTransform.position = Vector3.MoveTowards(objTransform.position, endPosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        obj.SetActive(false);

        isMoving = false;
    }
}
