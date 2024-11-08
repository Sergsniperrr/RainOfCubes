using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CubeView))]
public class Cube : SpawnableObject<Cube>
{
    private CubeView _view;

    private readonly float _delay = 1f;
    private bool _isCollided;

    private Coroutine _coroutine;

    private void Awake()
    {
        _view = GetComponent<CubeView>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollided || collision.gameObject.TryGetComponent(out Platform _) == false)
            return;

        _view.SetRandomColor();
        RunLifeCounter();
        _isCollided = true;
    }

    private void RunLifeCounter()
    {
        StopCounter();
        StartCounter();
    }

    private void StopCounter()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void StartCounter()
    {
        _coroutine = StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        var wait = new WaitForSeconds(_delay);
        float lifeTime = RandomLifeTime;

        while (lifeTime > 0)
        {
            lifeTime--;
            yield return wait;
        }

        Die(this);
        StopCounter();
        _view.ResetColor();
        _isCollided = false;
    }
}