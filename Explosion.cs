using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField, Range(2f, 10f)] private float _radius = 4f;
    [SerializeField, Range(5f, 15f)] private float _power = 10f;
    [SerializeField, Range(0f, 5f)] private float _upForce = 2f;
    [SerializeField] private Spawner _spawner;

    private Vector3 _position;

    private void OnEnable()
    {
        _spawner.OnFinishSpawn += OnExplode;
    }

    private void OnDisable()
    {
        _spawner.OnFinishSpawn += OnExplode;
    }

    private void OnExplode(List<Cube> cubes)
    {
        Vector3 explosionPosition = cubes[0].transform.position;

        foreach (var cube in cubes)
            cube.Rigidbody.AddExplosionForce(_power, explosionPosition, _radius, _upForce, ForceMode.Impulse);
    }
}