using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrow : MonoBehaviour
{
    private GameObject playerHandLoc;
    private Collider RubbleCol;
    private PlayerInventory PlayerInventory;
    private Rigidbody RubbleRb;

    [SerializeField] public float Damage;

    void Start()
    {
        playerHandLoc = GameObject.FindGameObjectWithTag("HoldLocation");
        RubbleRb = GetComponent<Rigidbody>();
        RubbleCol = GetComponent<Collider>();
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
    }     

    public void Pickup(){

        if ((PlayerInventory.NumberOfRubble < 5) && (RubbleRb.isKinematic == false)){
        RubbleCol.enabled = !RubbleCol.enabled;
        RubbleRb.isKinematic = true;
        this.gameObject.transform.parent=playerHandLoc.transform;   // Change to hand of Player
        transform.position = playerHandLoc.transform.position;      // Change to HoldLocation parent
        PlayerInventory.RubbleCollected();
        }

    }
}
