using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    public void Explode(Cube cube)
    {
        float explosionForce = 100;
        float overlapRadius = 3;
        float overlapRate = cube.Rigidbody.transform.localScale.x;

        Vector3 position = cube.Rigidbody.position;
        Collider[] colliders = Physics.OverlapSphere(position, overlapRadius / overlapRate);

        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.attachedRigidbody;

            if (rigidbody != null && rigidbody != cube.Rigidbody)
            {
                rigidbody.AddExplosionForce(explosionForce / overlapRate, position, overlapRadius / overlapRate);
            }
        }
    }

    public void Explode(List<Cube> cubes)
    {
        float explosionForce = 1000;
        float overlapRadius = 5;

        foreach (Cube cube in cubes)
        {
            cube.Rigidbody.AddExplosionForce(explosionForce, transform.position, overlapRadius);
        }
    }
}
