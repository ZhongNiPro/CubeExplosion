using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;

    private List<Cube> _newCubes;

    private void Start()
    {
        _newCubes = new List<Cube>();
    }

    private void OnEnable()
    {
        _cube.CubeSelected += Split;
    }

    private void OnDisable()
    {
        _cube.CubeSelected -= Split;
    }

    private void Split(GameObject currentCube, float scaleValue)
    {
        int minCount = 2;
        int maxCount = 6;
        int scaleRate = 2;
        float correctionRate = 0.5f;

        int randomCount = Random.Range(minCount, maxCount + 1);

        Vector3 explodePosition = currentCube.transform.position;

        List<Cube> splitedCubes = new List<Cube>();

        if (scaleValue >= Random.value)
        {
            scaleValue /= scaleRate;

            for (int i = 0; i < randomCount; i++)
            {
                float randomX = Random.value + currentCube.transform.position.x - correctionRate;
                float randomY = Random.value + currentCube.transform.position.y;
                float randomZ = Random.value + currentCube.transform.position.z - correctionRate;

                Vector3 position = new Vector3(randomX, randomY, randomZ);

                Cube newCube = Instantiate(_cube, position, Quaternion.identity);

                newCube.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);

                if (newCube.GetComponent<Renderer>() != null)
                    newCube.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);

                splitedCubes.Add(newCube);
            }
        }

        Explode(splitedCubes, explodePosition);
        _newCubes.AddRange(splitedCubes);
    }

    private void Explode(List<Cube> cubes, Vector3 position)
    {
        float explosionForce = 1000;
        float explosionRadius = 10;

        foreach (Cube cube in cubes)
        {
            cube.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, position, explosionRadius);
        }
    }
}
