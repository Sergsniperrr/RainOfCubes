using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    [SerializeField] private Cube _prefab;
    [SerializeField] private GameObject _zeroSpawnPoint;
    [SerializeField] private GameObject[] _ignoredObjects;

    private float _shift = 3f;
    private float _repeateRate = 1f;
    private int _poolCapacity = 5;
    private int _poolMaxSize = 8;
    private ObjectPool<Cube> _pool;

    private Vector3 _randomStartPoint => _zeroSpawnPoint.transform.position + new Vector3(0f, 0f, Random.Range(-_shift, _shift));

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        IgnoreObjects();

        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = _randomStartPoint;
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cube.gameObject.SetActive(true);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeateRate);
    }

    private void GetCube()
    {
        _pool.Get().AssignNewLifeTime();
    }

    private void IgnoreObjects()
    {
        foreach (GameObject gameObject in _ignoredObjects)
        {
            if (gameObject.GetComponent<Collider>())
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), _prefab.GetComponent<Collider>(), true);
        }
    }

    public void ReleaseCube(Cube cube)
    {
        _pool.Release(cube);
    }
}
