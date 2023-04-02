using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrow : MonoBehaviour
{
    public float radius = 4f;
    private Rigidbody RubbleRb;
    private GameObject playerHandLoc;
    private Collider ObjectCollider;
    private PlayerInventory PlayerInventory;
    [SerializeField] public float Damage;

    void Start()
    {
        playerHandLoc = GameObject.FindGameObjectWithTag("HoldLocation");
        RubbleRb = GetComponent<Rigidbody>();
        PlayerInventory = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerInventory>();
    }

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

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider c in colliders)
        {
            if (c.GetComponent<PlayerMovement>())
            {
                //adding picking up hat
                if(Input.GetKeyDown(KeyCode.F) && (PlayerInventory.NumberOfRubble < 5) && (RubbleRb.isKinematic == false))
                {
                    RubbleRb.isKinematic = true;
                    this.gameObject.transform.parent=playerHandLoc.transform;   // Change to hand of Player
                    transform.position = playerHandLoc.transform.position;      // Change to HoldLocation parent
                    PlayerInventory.RubbleCollected();
                } 
            }
        }
    }     
}
