using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CubeView))]
public class Cube : SpawnableObject
{
    private CubeView _view;

    private readonly float _delay = 1f;
    private readonly string _platformTag = "Platform";

    private Coroutine _coroutine;
    private GameObject _colisionBufer;

    private void Awake()
    {
        _view = GetComponent<CubeView>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _colisionBufer || collision.gameObject.transform.CompareTag(_platformTag) == false)
            return;

        _view.ChangeColor();
        RunLifeCounter();

        _colisionBufer = collision.gameObject;
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

        Die();
        StopCounter();
        _view.UpdateColors();
    }
}