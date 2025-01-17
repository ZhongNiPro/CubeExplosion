using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _spawner.CubeCreated += Explode;
    }

    private void OnDisable()
    {
        _spawner.CubeCreated -= Explode;
    }

    private void Explode(List<CubePrefab> cubes, Vector3 position)
    {
        float explosionForce = 1000;
        float explosionRadius = 10;

        foreach (CubePrefab cube in cubes)
        {
            cube.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, position, explosionRadius);
        }
    }
}
