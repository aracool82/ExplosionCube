using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private List<Cube> _fromExposionCubes = new List<Cube>();
    private int _maxCount = 6;
    private int _minCount = 2;

    public event Action<List<Cube>> OnFinishSpawn;

    private void Start()
    {
        int cubeCount = 4;
        int maxChance = 100;

        for (int i = 0; i < cubeCount; i++)
            CreateCube(_cubePrefab, maxChance, Vector3.one);
    }

    private Cube CreateCube(Cube cube, int chance, Vector3 scale)
    {
        float offset = cube.transform.localScale.x;
        Vector3 position = cube.transform.position + Random.onUnitSphere * offset;

        Cube newCube = Instantiate(cube, position + Vector3.up, Random.rotation);
        newCube.Initialize(chance, scale);

        newCube.OnClicked += ChangeParameters;
        
        return newCube;
    }

    private void ChangeParameters(Cube cube)
    {
        int divider = 2;

        int chance = cube.ChanceSplit / divider;
        Vector3 scale = cube.transform.localScale / divider;

        _fromExposionCubes.Clear();
        int count = Random.Range(_minCount, _maxCount + 1);

        for (int i = 0; i < count; i++)
            _fromExposionCubes.Add(CreateCube(cube, chance, scale));

        OnFinishSpawn?.Invoke(_fromExposionCubes);
    }
}