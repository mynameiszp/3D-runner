using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private int obstacleSpawnIndex;
    private ObjectsPool objectsPool;
    private Vector3 initialPosition;
    private bool hasStarted;
    [SerializeField] private GameObject positionsForSpawn;
    [SerializeField] private float spawnInitialPosition = 30f;
    void Start()
    {
        objectsPool = ObjectsPool.Instance;
    }

    void Update()
    {
        if (PlayerMove.Instance.PlayMode)
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
        GameObject gameObject = objectsPool.GetPooledObject();
        if (gameObject != null)
        {
            if (!gameObject.activeInHierarchy)
            {
                initialPosition = positionsForSpawn.transform.GetChild(obstacleSpawnIndex).position;
                gameObject.SetActive(true);
                gameObject.transform.position = new Vector3(initialPosition.x, initialPosition.y, spawnInitialPosition);
                spawnInitialPosition += 10f;
            }
        }
    }
    private void DeactivateObject()
    {
        foreach (GameObject gameObject in objectsPool.GetPooledObjects())
        {
            if (gameObject.transform.position.z < PlayerMove.Instance.transform.position.z - 2)
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
