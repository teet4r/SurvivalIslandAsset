using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletCtrl : MonoBehaviour
{
    Rigidbody _rigidbody;
    TrailRenderer _trailRenderer;

    public float speed;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _trailRenderer = GetComponent<TrailRenderer>();

        if (speed <= 0f) speed = 100f;
    }

    void OnEnable()
    {
        _rigidbody.AddForce(transform.forward * speed);
    }

    void OnDisable()
    {
        _rigidbody.velocity = Vector3.zero;
        _trailRenderer.Clear();
    }
}