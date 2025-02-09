using System;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    public event Action<Cube> Destroyed;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Interact()
    {
        Destroyed?.Invoke(this);
        Destroy(gameObject);
    }

    public void ChangeColor()
    {
        _renderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    public void Explode(List<Cube> cubes)
    {
        float explosionForce = 500;
        float explosionRadius = 10;

        foreach (Cube cube in cubes)
        {
            cube._rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
    }
}
