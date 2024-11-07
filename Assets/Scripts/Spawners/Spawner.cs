using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : SpawnableObject<T>
{
    [SerializeField] private T _prefab;

    private int _poolCapacity = 5;
    private int _poolMaxSize = 8;
    private ObjectPool<T> _pool;

    public event Action ObjectCreated;
    public event Action ObjectSpawned;
    public event Action<bool> ObjectActiveChanged;

    protected abstract Vector3 SpawnPoint { get; }

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Create(),
            actionOnGet: (spawnableObject) => HandleObjectSpawning(spawnableObject),
            actionOnRelease: (spawnableObject) => spawnableObject.gameObject.SetActive(false),
            actionOnDestroy: (spawnableObject) => Destroy(spawnableObject.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    protected void GetObject()
    {
        _pool.Get().Died += ReleaseObject;
    }

    protected virtual void HandleObjectSpawning(T spawnableObject)
    {
        spawnableObject.transform.position = SpawnPoint;

        if (spawnableObject.TryGetComponent(out Rigidbody rigidbody))
            rigidbody.velocity = Vector3.zero;

        spawnableObject.gameObject.SetActive(true);

        ObjectActiveChanged?.Invoke(true);
        ObjectSpawned?.Invoke();
    }

    protected virtual void ReleaseObject(T spawnableObject)
    {
        _pool.Release(spawnableObject);
        spawnableObject.Died -= ReleaseObject;

        ObjectActiveChanged?.Invoke(false);
    }

    private T Create()
    {
        ObjectCreated?.Invoke();

        return Instantiate(_prefab);
    }
}
