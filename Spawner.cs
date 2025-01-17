using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubePrefab _cubePrefab;
    [SerializeField] private CubeSelector _cubeSelector;

    private List<CubePrefab> _newCubes;

    public event Action<List<CubePrefab>, Vector3> CubeCreated;

    private void Start()
    {
        _newCubes = new List<CubePrefab>();
    }

    private void OnEnable()
    {
        _cubeSelector.CubeSelected += Split;
    }

    private void OnDisable()
    {
        _cubeSelector.CubeSelected -= Split;
    }

    private void Split(GameObject currentCube, float scaleValue)
    {
        int minCount = 2;
        int maxCount = 6;
        int scaleRate = 2;
        float correctionRate = 0.5f;

        int randomCount = UnityEngine.Random.Range(minCount, maxCount + 1);

        Vector3 explodePosition = currentCube.transform.position;

        List<CubePrefab> splitedCubes = new List<CubePrefab>();

        if (scaleValue >= UnityEngine.Random.value)
        {
            scaleValue /= scaleRate;

            for (int i = 0; i < randomCount; i++)
            {
                float randomX = UnityEngine.Random.value + currentCube.transform.position.x - correctionRate;
                float randomY = UnityEngine.Random.value + currentCube.transform.position.y;
                float randomZ = UnityEngine.Random.value + currentCube.transform.position.z - correctionRate;

                Vector3 position = new Vector3(randomX, randomY, randomZ);

                CubePrefab newCube = Instantiate(_cubePrefab, position, Quaternion.identity);

                newCube.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);

                if (newCube.GetComponent<Renderer>() != null)
                    newCube.GetComponent<Renderer>().material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);

                splitedCubes.Add(newCube);
            }
        }

        CubeCreated?.Invoke(splitedCubes, explodePosition);
        _newCubes.AddRange(splitedCubes);
    }
}
