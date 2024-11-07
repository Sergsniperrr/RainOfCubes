using System;
using UnityEngine;

public class SpawnerStats<T> : MonoBehaviour where T : SpawnableObject<T>
{
    [SerializeField] protected Spawner<T> _spawner;

    private int _createdObjectsCounter;
    private int _spawnedObjectsCounter;
    private int _activeObjectsCounter;

    public event Action<int> CreatedObjectsCounterChanged;
    public event Action<int> SpawnedObjectsCounterChanged;
    public event Action<int> ActiveObjectsCounterChanged;

    private void OnEnable()
    {
        _spawner.ObjectCreated += ChangeCreatedObjectsCounter;
        _spawner.ObjectSpawned += ChangeSpawnedObjectsCounter;
        _spawner.ObjectActiveChanged += ChangeActiveObjectsCounter;
    }

    private void OnDisable()
    {
        _spawner.ObjectCreated -= ChangeCreatedObjectsCounter;
        _spawner.ObjectSpawned -= ChangeSpawnedObjectsCounter;
        _spawner.ObjectActiveChanged -= ChangeActiveObjectsCounter;
    }

    private void ChangeCreatedObjectsCounter()
    {
        _createdObjectsCounter++;
        CreatedObjectsCounterChanged?.Invoke(_createdObjectsCounter);
    }

    private void ChangeSpawnedObjectsCounter()
    {
        _spawnedObjectsCounter++;
        SpawnedObjectsCounterChanged?.Invoke(_spawnedObjectsCounter);
    }

    private void ChangeActiveObjectsCounter(bool isActive)
    {
        if (isActive)
            _activeObjectsCounter++;
        else
            _activeObjectsCounter--;

        ActiveObjectsCounterChanged?.Invoke(_activeObjectsCounter);
    }
}
