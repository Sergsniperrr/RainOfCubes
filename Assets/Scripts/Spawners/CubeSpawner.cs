using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    private float _shiftOnZ = 3f;
    private float _shiftOnX = 12f;
    private float _repeateRate = 1f;
    private Coroutine _coroutine;

    public event Action<Vector3> CubeReleased;

    protected override Vector3 SpawnPoint => transform.position + new Vector3(RandomShiftOnX, 0f, RandomShiftOnZ);

    private float RandomShiftOnZ => UnityEngine.Random.Range(-_shiftOnZ, _shiftOnZ);
    private float RandomShiftOnX => UnityEngine.Random.Range(0f, _shiftOnX);

    private void Start()
    {
        _coroutine = StartCoroutine(RepeatSpawn());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    protected override void ReleaseObject(Cube cube)
    {
        CubeReleased?.Invoke(cube.transform.position);

        base.ReleaseObject(cube);
    }

    private IEnumerator RepeatSpawn()
    {
        bool canSpawn = true;
        var interval = new WaitForSeconds(_repeateRate);

        while (canSpawn)
        {
            GetObject();

            yield return interval;
        }
    }
}