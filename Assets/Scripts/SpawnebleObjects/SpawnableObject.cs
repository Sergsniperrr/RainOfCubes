using System;
using UnityEngine;

public abstract class SpawnableObject<T> : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2;
    [SerializeField] private float _maxLifeTime = 5;

    protected float RandomLifeTime => UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);

    public event Action<T> Died;

    protected void Die(T self)
    {
        Died?.Invoke(self);
    }
}
