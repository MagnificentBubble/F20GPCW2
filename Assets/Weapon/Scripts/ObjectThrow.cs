using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrow : MonoBehaviour
{

    private Rigidbody ObjectRigidBody;
    private Collider ObjectCollider;    

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

            ObjectRigidBody.isKinematic = true;
            ObjectCollider.isTrigger = true;
        } 
    
    }
        
}
