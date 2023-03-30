using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrow : MonoBehaviour
{

    private Rigidbody ObjectRigidBody;
    private Collider ObjectCollider;
    [SerializeField] public float Damage;

    void OnCollisionEnter(Collision collision)
    {
        // If it misses the enemy and hits the floor
        if (collision.gameObject.tag == "Floor")
        {
            ObjectRigidBody.isKinematic = true;
            ObjectCollider.isTrigger = true;
        } 
        // If it hits the player
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().Hurt(Damage);

            ObjectRigidBody.isKinematic = true;
            ObjectCollider.isTrigger = true;
        }
        // If it hits the player
        if (collision.gameObject.tag == "Destructable")
        {
            collision.gameObject.GetComponent<Health>().Hurt(Damage);

            ObjectRigidBody.isKinematic = true;
            ObjectCollider.isTrigger = true;
        }

    }
        
}
