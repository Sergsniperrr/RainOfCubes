using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private Vector3 _spawnPoint;

    protected override Vector3 SpawnPoint => _spawnPoint;

    private void OnEnable()
    {
        _cubeSpawner.CubeReleased += Spawn;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeReleased -= Spawn;
    }

    private void Spawn(Vector3 deadPointOfCube)
    {
        _spawnPoint = deadPointOfCube;

        GetObject();
    }

    protected override void HandleObjectSpawning(Bomb bomb)
    {
        base.HandleObjectSpawning(bomb);
        bomb.Activate();
    }
}
