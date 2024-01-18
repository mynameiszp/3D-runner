using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject positionsForSpawn;
    [SerializeField] private float spawnInitialPosition = 100f;
    [SerializeField] private float spawnObjectDistance = 10f;
    private int obstacleSpawnIndex;
    private ObjectsPool objectsPool;
    private Vector3 initialPosition;
    public bool hasStartedGame;
    public bool HasRestartedGame { get; set; }
    public static ObstacleSpawner Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        objectsPool = ObjectsPool.Instance;
    }

    void Update()
    {
        if (PlayerController.Instance.PlayMode)
        {
            if (!hasStartedGame) StartCoroutine(WaitToStart(4));

            if (hasStartedGame)
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
                if (!currentGameObject.activeInHierarchy && !previousGameObject.activeInHierarchy)
                {
                    initialPosition = positionsForSpawn.transform.GetChild(obstacleSpawnIndex).position;
                    currentGameObject.SetActive(true);
                    currentGameObject.transform.position = new Vector3(initialPosition.x, initialPosition.y, spawnInitialPosition + spawnObjectDistance);
                }
                else if (!currentGameObject.activeInHierarchy)
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
            if (gameObject.transform.position.z < PlayerController.Instance.transform.position.z - 2)
            {
                objectsPool.DeactivateObject(gameObject);
            }
        }
    }

    private IEnumerator WaitToStart(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        hasStartedGame = true;
    }
}