using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private readonly float _minLifeTime = 2;
    private readonly float _maxLifeTime = 5;
    private readonly float delay = 1f;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _currentLifeTime;

    private Coroutine _coroutine;
    private GameObject _colisionBufer;
    private Dictionary<string, Color> _colors = new Dictionary<string, Color>()
    {
        { "TopPlatform", Color.green },
        { "MiddlePlatform", Color.yellow },
        { "BottomPlatform", Color.red },
        { "FinishPlatform", Color.white }
    };

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _colisionBufer || _colors.ContainsKey(collision.gameObject.name) == false)
            return;

        ChangeColor(collision.gameObject.name);
        RunLifeCounter();

        _colisionBufer = collision.gameObject;
    }

    private void ChangeColor(string key)
    {
        GetComponent<Renderer>().material.color = _colors[key];
    }

    private void RunLifeCounter()
    {
        StopCounter();
        AssignNewLifeTime();
        StartCounter();
    }

    private void StopCounter()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void AssignNewLifeTime()
    {
        _currentLifeTime = Random.Range(_minLifeTime, _maxLifeTime);
    }

    private void StartCounter()
    {
        _coroutine = StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        var wait = new WaitForSeconds(delay);

        while (_currentLifeTime > 0)
        {
            _currentLifeTime--;
            yield return wait;
        }

        DestroyCube();
        StopCounter();
    }

    private void DestroyCube()
    {
        Spawner.Instance.ReleaseCube(this);       
    }
}