using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletCtrl : MonoBehaviour
{
    [Min(1f)][SerializeField] float speed = 1000f;

    Rigidbody _rigidbody;
    TrailRenderer _trailRenderer;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _trailRenderer = GetComponent<TrailRenderer>();
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