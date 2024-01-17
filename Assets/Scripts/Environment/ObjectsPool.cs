using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> pooledObjects;
    [SerializeField] private int objectsAmount;
    [SerializeField] private List<GameObject> objectPrefabs;
    public static ObjectsPool Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        pooledObjects = new List<GameObject>();
    }
    void Start()
    {
        GameObject temp;
        for (int i = 0; i < objectsAmount; i++)
        {
            temp = Instantiate(objectPrefabs[Random.Range(0, objectPrefabs.Count)]);
            temp.AddComponent<ObstacleMovement>();
            temp.SetActive(false);
            pooledObjects.Add(temp);
        }
    }

    public int GetPooledObjectIndex()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }

    public List<GameObject> GetPooledObjects()
    {
        return pooledObjects;
    }

    public GameObject GetPreviousObject(int currentIndex)
    {
        if (currentIndex == 0) return pooledObjects[pooledObjects.Count - 1];
        if (currentIndex < pooledObjects.Count) return pooledObjects[currentIndex - 1];
        return null;
    }

    public void DeactivateObject(GameObject obj)
    {
        if (pooledObjects.Contains(obj)) obj.SetActive(false);
    }
    public void DeactivateAll()
    {
        foreach(GameObject obj in pooledObjects)
        {
            obj.SetActive(false);
        }
    }

    public void ActivateAll()
    {
        foreach (GameObject obj in pooledObjects)
        {
            obj.SetActive(false);
        }
    }
}