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
    private Transform playerMeleeLoc;

    void Start()
    {
        // Get component
        Rigidbody = GetComponent<Rigidbody>();
        MeshCollider = GetComponent<MeshCollider>();
        playerMeleeLoc = GameObject.Find("MeleeWeaponContainer").GetComponent<Transform>();

        // Set variables
        Rigidbody.isKinematic = true;
        MeshCollider.convex = true;
        MeshCollider.isTrigger = true;
        InflictMeleeDamage = false;
    }

    void Update()
    {
        // Test
        if(Input.GetKeyDown(KeyCode.G))
        {
            Drop();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Destructable" && InflictMeleeDamage)
        {
            collision.gameObject.GetComponent<Health>().Hurt(WeaponDamage);
            InflictMeleeDamage = false;
        }
    }

    public void Pickup()
    {   
        // Set Rigidbody and trigger to true
        Rigidbody.isKinematic = true;
        MeshCollider.isTrigger = true;

        // Pick melee to player hand
        this.gameObject.transform.parent=playerMeleeLoc.transform;   // Change paremt to left hand of player
        transform.position = playerMeleeLoc.transform.position;      // Change location to player
        transform.rotation = playerMeleeLoc.transform.rotation;     // Change rotation to player
        
    }
    
    public void Drop()
    {
        // Set Rigidbody and trigger to false
        Rigidbody.isKinematic = false;
        MeshCollider.isTrigger = false;

        // Drop melee weapon
        this.gameObject.transform.SetParent(null);   // Move to hierachy
    }

}
