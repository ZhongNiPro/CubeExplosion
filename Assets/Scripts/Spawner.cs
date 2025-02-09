using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Cube _cubePrefab;

    private void OnEnable()
    {
        _cube.Destroyed += OnCubeDestroyed;
    }

    private void OnDisable()
    {
        _cube.Destroyed -= OnCubeDestroyed;
    }

    private void OnCubeDestroyed(Cube cube)
    {
        Debug.Log("!");
        cube.Destroyed -= OnCubeDestroyed;

        int minCount = 2;
        int maxCount = 6;
        int scaleRate = 2;
        float correctionRate = 0.5f;

        int randomCount = Random.Range(minCount, maxCount + 1);
        List<Cube> splitedCubes = new List<Cube>();
        float scaleValue = cube.transform.localScale.x;

        if (scaleValue >= Random.value)
        {
            scaleValue /= scaleRate;

            for (int i = 0; i < randomCount; i++)
            {
                float randomX = Random.value + cube.transform.position.x - correctionRate;
                float randomY = Random.value + cube.transform.position.y;
                float randomZ = Random.value + cube.transform.position.z - correctionRate;

                Vector3 position = new Vector3(randomX, randomY, randomZ);

                Cube newCube = Instantiate(_cubePrefab, position, Quaternion.identity);

                newCube.Destroyed += OnCubeDestroyed;

                newCube.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);

                newCube.ChangeColor();

                splitedCubes.Add(newCube);
            }
        }

        cube.Explode(splitedCubes);
        cube.Destroyed -= OnCubeDestroyed;

    }
}
