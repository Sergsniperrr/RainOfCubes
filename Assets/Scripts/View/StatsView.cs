using UnityEngine;
using TMPro;

public class StatsView<T> : MonoBehaviour where T : SpawnableObject<T>
{
    [SerializeField] protected SpawnerStats<T> Counters;
    [SerializeField] private TMP_Text _createdObjects;
    [SerializeField] private TMP_Text _spawnedObjects;
    [SerializeField] private TMP_Text _activeObjects;

    private void OnEnable()
    {
        Counters.CreatedObjectsCounterChanged += ShowCreatedObject;
        Counters.SpawnedObjectsCounterChanged += ShowSpawnedObject;
        Counters.ActiveObjectsCounterChanged += ShowActiveObject;
    }

    private void OnDisable()
    {
        Counters.CreatedObjectsCounterChanged -= ShowCreatedObject;
        Counters.SpawnedObjectsCounterChanged -= ShowSpawnedObject;
        Counters.ActiveObjectsCounterChanged -= ShowActiveObject;
    }

    protected void ShowSpawnedObject(int counter)
    {
        _spawnedObjects.text = counter.ToString();
    }

    private void ShowCreatedObject(int counter)
    {
        _createdObjects.text = counter.ToString();
    }

    private void ShowActiveObject(int counter)
    {
        _activeObjects.text = counter.ToString();
    }
}
