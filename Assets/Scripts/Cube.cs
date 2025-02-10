using System;
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

    public void Explode()
    {
        float explosionForce = 2;
        float overlapRadius = 3;
        float overlapRate = this._rigidbody.transform.localScale.x;

        Vector3 position = this._rigidbody.position;
        Collider[] colliders = Physics.OverlapSphere(position, overlapRadius / overlapRate);

        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.attachedRigidbody;

            if (rigidbody != null && rigidbody != this._rigidbody)
            {
                Vector3 direction = (rigidbody.position - position).normalized;
                float distance = (rigidbody.position - position).sqrMagnitude;
                Vector3 force = explosionForce / overlapRate * rigidbody.mass * direction / distance;

                rigidbody.AddForceAtPosition(force, position, ForceMode.Impulse);
            }
        }
    }
}
