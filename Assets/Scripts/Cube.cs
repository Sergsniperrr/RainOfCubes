using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    private readonly float _minLifeTime = 2;
    private readonly float _maxLifeTime = 5;
    private readonly float _delay = 1f;
    private readonly string _platformTag = "Platform";

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _currentLifeTime;

    private Coroutine _coroutine;
    private GameObject _colisionBufer;
    private Color[] _colors = {Color.green, Color.yellow, Color.red, Color.white };
    private Queue<Color> _colorAssigner = new Queue<Color>();

    public event Action<Cube> Died;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _colisionBufer || !collision.gameObject.transform.CompareTag(_platformTag))
            return;

        ChangeColor();
        RunLifeCounter();

        _colisionBufer = collision.gameObject;
    }

    private void ChangeColor()
    {
        if (_colorAssigner.Count == 0)
            _colorAssigner = new Queue<Color>(_colors);

        GetComponent<Renderer>().material.color = _colorAssigner.Dequeue();
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
        _currentLifeTime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
    }

    private void StartCounter()
    {
        _coroutine = StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        var wait = new WaitForSeconds(_delay);

        while (_currentLifeTime > 0)
        {
            _currentLifeTime--;
            yield return wait;
        }

        Died(this);
        StopCounter();
    }
}