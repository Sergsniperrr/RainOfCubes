using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnableObject _prefab;

    private int _poolCapacity = 5;
    private int _poolMaxSize = 8;
    private ObjectPool<SpawnableObject> _pool;

    public event Action ObjectCreated;
    public event Action ObjectSpawned;
    public event Action<bool> ObjectActiveChanged;

    protected abstract Vector3 SpawnPoint { get; }

    private void Awake()
    {
        _pool = new ObjectPool<SpawnableObject>(
            createFunc: () => Create(),
            actionOnGet: (spawnableObject) => ActionOnGet(spawnableObject),
            actionOnRelease: (spawnableObject) => spawnableObject.gameObject.SetActive(false),
            actionOnDestroy: (spawnableObject) => Destroy(spawnableObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    protected void GetObject()
    {
        _pool.Get().Died += ReleaseObject;
    }

    protected virtual void ActionOnGet(SpawnableObject spawnableObject)
    {
        spawnableObject.transform.position = SpawnPoint;

        if (spawnableObject.TryGetComponent(out Rigidbody rigidbody))
            rigidbody.velocity = Vector3.zero;

        spawnableObject.gameObject.SetActive(true);

        ObjectActiveChanged?.Invoke(true);
        ObjectSpawned?.Invoke();
    }

    protected virtual void ReleaseObject(SpawnableObject spawnableObject)
    {
        _pool.Release(spawnableObject);
        spawnableObject.Died -= ReleaseObject;

        ObjectActiveChanged?.Invoke(false);
    }

    private SpawnableObject Create()
    {
        ObjectCreated?.Invoke();

        return Instantiate(_prefab);
    }
}
