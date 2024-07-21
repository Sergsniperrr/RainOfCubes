using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;

    private float _shift = 3f;
    private float _repeateRate = 1f;
    private int _poolCapacity = 5;
    private int _poolMaxSize = 8;
    private ObjectPool<Cube> _pool;

    private Vector3 RandomStartPoint => transform.position + new Vector3(0f, 0f, Random.Range(-_shift, _shift));

    private void Awake()
    {
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
        cube.transform.position = RandomStartPoint;
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cube.gameObject.SetActive(true);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeateRate);
    }

    private void GetCube()
    {
        _pool.Get().Died += ReleaseCube;
    }

    private void ReleaseCube(Cube cube)
    {
        _pool.Release(cube);
        cube.Died -= ReleaseCube;
    }
}
