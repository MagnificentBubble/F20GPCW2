using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrow : MonoBehaviour
{
    private Collider ObjectCollider;
    [SerializeField] public float Damage;

    void OnCollisionEnter(Collision collision)
    {
        // If it hits the player
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().Hurt(Damage);
        }
        // If it hits the player
        if (collision.gameObject.tag == "Destructable")
        {
            collision.gameObject.GetComponent<Health>().Hurt(Damage);
        }
    }
        
}
