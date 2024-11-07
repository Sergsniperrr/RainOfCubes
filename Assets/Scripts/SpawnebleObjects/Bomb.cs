using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Explosion), typeof(BombView))]
public class Bomb : SpawnableObject<Bomb>
{
    private Explosion _explosion;
    private BombView _view;

    protected override Bomb GetSelf => this;

    private void Awake()
    {
        _explosion = GetComponent<Explosion>();
        _view = GetComponent<BombView>();
    }

    private void Start()
    {
        Activate();
    }

    public void Activate()
    {
        StartCoroutine(HandleExplosion());
    }

    private IEnumerator HandleExplosion()
    {
        float lifeTime = RandomLifeTime;
        float counter = lifeTime;

        while (counter > 0)
        {
            counter -= Time.deltaTime;

            _view.ChangeTransparency(counter / lifeTime);

            yield return null;
        }

        _explosion.Explode();

        Die();
        _view.ResetColor();
    }
}
