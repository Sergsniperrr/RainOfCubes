using System;
using UnityEngine;

public class CubeSpawner : Spawner
{
    private float _shift = 3f;
    private float _repeateRate = 1f;

    public event Action<Vector3> CubeReleased;

    protected override Vector3 SpawnPoint => transform.position + new Vector3(0f, 0f, UnityEngine.Random.Range(-_shift, _shift));

    private void Start()
    {
        InvokeRepeating(nameof(GetObject), 0.0f, _repeateRate);
    }

    protected override void ReleaseObject(SpawnableObject spawnableObject)
    {
        CubeReleased?.Invoke(spawnableObject.transform.position);

        base.ReleaseObject(spawnableObject);
    }
}
