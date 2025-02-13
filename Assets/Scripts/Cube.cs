using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    public float Chance { get; set; } = 1f;
    public Renderer Renderer => _renderer;
    public Rigidbody Rigidbody => _rigidbody;

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
}