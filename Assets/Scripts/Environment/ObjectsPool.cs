using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : MonoBehaviour
{
    public static ObjectsPool Instance;
    [SerializeField] private List<GameObject> pooledObjects;    
    [SerializeField] private int objectsAmount;
    [SerializeField] private List<GameObject> objectPrefabs;
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
            temp.AddComponent<ObstacleMove>();
            temp.SetActive(false);
            pooledObjects.Add(temp);
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public List<GameObject> GetPooledObjects()
    {
        return pooledObjects;
    }
}
