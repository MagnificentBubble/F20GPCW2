using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]

public class MeleeWeapon : MonoBehaviour
{
    // Set weapon damage
    public int WeaponDamage = 10;
    public bool InflictMeleeDamage;

    // Reference
    private Rigidbody Rigidbody;
    private MeshCollider MeshCollider;

    void Start()
    {
        // Get component
        Rigidbody = GetComponent<Rigidbody>();
        MeshCollider = GetComponent<MeshCollider>();

        // Set variables
        Rigidbody.isKinematic = true;
        MeshCollider.convex = true;
        MeshCollider.isTrigger = true;
        InflictMeleeDamage = false;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Destructable" && InflictMeleeDamage)
        {
            collision.gameObject.GetComponent<Health>().Hurt(WeaponDamage);
            InflictMeleeDamage = false;
        }
    }
}
