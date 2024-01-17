using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject positionsForSpawn;
    [SerializeField] private float spawnInitialPosition = 50f;
    [SerializeField] private float spawnObjectDistance = 10f;
    private int obstacleSpawnIndex;
    private ObjectsPool objectsPool;
    private Vector3 initialPosition;
    private bool hasStarted;
    void Start()
    {
        objectsPool = ObjectsPool.Instance;
    }

    void Update()
    {
        if (PlayerMovement.Instance.PlayMode)
        {
            StartCoroutine(WaitToStart());
            if (hasStarted)
            {
                GenerateObjects();
                DeactivateObject();
            }
        }
    }

    private void GenerateObjects()
    {
        obstacleSpawnIndex = Random.Range(0, 3);
        int objectIndex = objectsPool.GetPooledObjectIndex();
        if (objectIndex != -1)
        {
            GameObject currentGameObject = objectsPool.GetPooledObjects()[objectIndex];
            GameObject previousGameObject = objectsPool.GetPreviousObject(objectIndex);
            if (currentGameObject != null && previousGameObject != null)
            {
                if (!currentGameObject.activeInHierarchy)
                {
                    initialPosition = positionsForSpawn.transform.GetChild(obstacleSpawnIndex).position;
                    currentGameObject.SetActive(true);
                    currentGameObject.transform.position = new Vector3(initialPosition.x, initialPosition.y, previousGameObject.transform.position.z + spawnObjectDistance);
                }
            }
        }
    }

    private void DeactivateObject()
    {
        foreach (GameObject gameObject in objectsPool.GetPooledObjects())
        {
            if (gameObject.transform.position.z < PlayerMovement.Instance.transform.position.z - 2)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator WaitToStart()
    {
        if (!hasStarted)
        {
            yield return new WaitForSecondsRealtime(4);
            hasStarted = true;
        }
    }
}