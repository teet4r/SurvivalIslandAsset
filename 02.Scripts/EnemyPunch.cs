using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyPunch : MonoBehaviour
{
    [SerializeField]
    int damage;

    SphereCollider sphereCollider;

    void Awake()
    {
        tag = "Punch";
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
    }

    public int GetDamage() { return damage; }
}