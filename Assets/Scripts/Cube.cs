using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private readonly float _minLifeTime = 2;
    private readonly float _maxLifeTime = 5;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _currentLifeTime;

    private GameObject _colisionBufer;

    private void Update()
    {
        if ((_currentLifeTime -= Time.deltaTime) <= 0)
            DestroyCube();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _colisionBufer)
            return;

        AssignNewLifeTime();

        _colisionBufer = collision.gameObject;
    }

    private void DestroyCube()
    {
        Spawner.Instance.ReleaseCube(this);
    }

    public void AssignNewLifeTime()
    {
        _currentLifeTime = Random.Range(_minLifeTime, _maxLifeTime);
    }
}






























