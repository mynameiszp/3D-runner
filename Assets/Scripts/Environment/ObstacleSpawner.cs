using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject positionsForSpawn;
    [SerializeField] private float spawnInitialPosition = 100f;
    [SerializeField] private float spawnObjectDistance = 30f;
    private int _obstacleSpawnIndex;
    private bool _passedFirstObject;
    private ObjectsPool _objectsPool;
    private Vector3 _initialPosition;
    //public bool hasStartedGame;
    public bool HasRestartedGame { get; set; }
    public static ObstacleSpawner Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    void Start()
    {
        _objectsPool = ObjectsPool.Instance;
    }

    void Update()
    {
        if (PlayerController.Instance.PlayMode)
        {
            //if (!hasStartedGame) StartCoroutine(WaitToStart(4));

            //if (!hasStartedGame)
            //{
                GenerateObjects();
                DeactivateObject();
            //}
        }
    }

    private void GenerateObjects()
    {
        _obstacleSpawnIndex = Random.Range(0, 3);
        int objectIndex = _objectsPool.GetPooledObjectIndex();
        float z = 0f;
        if (objectIndex != -1)
        {
            GameObject currentGameObject = _objectsPool.GetPooledObjects()[objectIndex];
            GameObject previousGameObject = _objectsPool.GetPreviousObject(objectIndex);           
            if (currentGameObject != null && previousGameObject != null)
            {
                if (!currentGameObject.activeInHierarchy)
                {
                    _initialPosition = positionsForSpawn.transform.GetChild(_obstacleSpawnIndex).position;
                    currentGameObject.SetActive(true);
                    z = previousGameObject.activeInHierarchy ? previousGameObject.transform.position.z + spawnObjectDistance : 50f;
                    currentGameObject.transform.position = new Vector3(_initialPosition.x, currentGameObject.transform.position.y, z);
                }
            }
        }
    }

    private void DeactivateObject()
    {
        foreach (GameObject gameObject in _objectsPool.GetPooledObjects())
        {
            if (gameObject.transform.position.z < PlayerController.Instance.transform.position.z - 2)
            {
                _objectsPool.DeactivateObject(gameObject);
                _passedFirstObject = true;
            }
        }
    }

    //private IEnumerator WaitToStart(float time)
    //{
    //    yield return new WaitForSecondsRealtime(time);
    //    Debug.Log("Timer");
    //    hasStartedGame = true;
    //}

    public bool GetPassedFirstObject()
    {
        return _passedFirstObject;
    }
}