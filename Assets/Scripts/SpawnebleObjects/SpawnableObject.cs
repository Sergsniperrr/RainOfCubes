using System;
using UnityEngine;

public abstract class SpawnableObject<T> : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2;
    [SerializeField] private float _maxLifeTime = 5;

    protected abstract T GetSelf { get; }
    protected float RandomLifeTime => UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);

    public event Action<T> Died;

    protected void Die()
    {
        Died?.Invoke(GetSelf);
    }

    //protected abstract T GetSelf();
}
