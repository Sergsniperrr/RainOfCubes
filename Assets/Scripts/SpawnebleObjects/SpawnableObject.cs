using System;
using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2;
    [SerializeField] private float _maxLifeTime = 5;

    protected float RandomLifeTime => UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);

    public event Action<SpawnableObject> Died;

    protected void Die()
    {
        Died?.Invoke(this);
    }
}
