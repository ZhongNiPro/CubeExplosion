using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private CubeColor _cubeColor;
    private Exploder _exploder;

    private void Awake()
    {
        _cubeColor = gameObject.AddComponent<CubeColor>();
        _exploder = gameObject.AddComponent<Exploder>();
    }

    private void OnEnable()
    {
        _cubePrefab.Destroyed += OnCubeDestroyed;
    }

    private void OnDisable()
    {
        _cubePrefab.Destroyed -= OnCubeDestroyed;
    }

    private void OnCubeDestroyed(Cube destroyedCube)
    {
        destroyedCube.Destroyed -= OnCubeDestroyed;

        Cube cubePrefab = _cubePrefab;

        int minCount = 2;
        int maxCount = 6;
        int scaleRate = 2;
        float divisionRate = 2f;
        float correctionRate = 0.5f;

        int randomCount = Random.Range(minCount, maxCount + 1);
        if (destroyedCube.Chance >= Random.value && _cubePrefab !=null)
        {
            List<Cube> cubes = new();

            for (int i = 0; i < randomCount; i++)
            {
                float randomX = Random.value + destroyedCube.transform.position.x - correctionRate;
                float randomY = Random.value + destroyedCube.transform.position.y;
                float randomZ = Random.value + destroyedCube.transform.position.z - correctionRate;

                Vector3 position = new Vector3(randomX, randomY, randomZ);

                Cube cube = Instantiate(_cubePrefab, position, Quaternion.identity);

                cube.Chance = destroyedCube.Chance / divisionRate;

                cube.Destroyed += OnCubeDestroyed;

                cube.transform.localScale = destroyedCube.transform.localScale / scaleRate;

                _cubeColor.ChangeColor(cube);

                cubes.Add(cube);
            }

            _exploder.Explode(cubes);
        }
        else
        {
            _exploder.Explode(destroyedCube);
        }
    }
}