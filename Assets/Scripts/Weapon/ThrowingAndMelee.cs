using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]

public class ThrowingAndMelee : MonoBehaviour
{
    private PlayerInventory PlayerInventory;

    [Header("References")]
    public Transform Player;
    public Transform AttackPoint;
    private GameObject ObjectToThrow;


    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse1;
    public float throwForce;
    public float throwUpwardForce;

    [Header("Melee")]
    public KeyCode MeleeKey = KeyCode.Mouse0;
    private Animator MeleeAnimator;
    private MeleeWeapon MeleeWeapon;
    

    bool readyToThrow;

    private void Start()
        {
            readyToThrow = true;
            PlayerInventory = GetComponent<PlayerInventory>();
            MeleeAnimator = GameObject.FindGameObjectWithTag("MeleeLocation").GetComponentInParent<Animator>();
            MeleeWeapon = GameObject.FindGameObjectWithTag("MeleeWeapon").GetComponent<MeleeWeapon>();
        }

    private void Update()
    {
        // Throw when number of rubble is not zero
        if((Input.GetKeyDown(throwKey) == true) && (readyToThrow == true) && (PlayerInventory.childExists == true))
        {
            Throw();
        }

        // Melee when number of rubble is not zero
        if(Input.GetKeyDown(MeleeKey))
        {
            MeleeWeapon.InflictMeleeDamage = true;
            MeleeAnimator.SetTrigger("isMelee");
        }
    }

    private void Throw()
    {        
        readyToThrow = false;
        
        // Refererences
        Collider ObjectCollider = GameObject.FindGameObjectWithTag("HoldLocation").GetComponentInChildren<Collider>();
        Rigidbody projectileRb = GameObject.FindGameObjectWithTag("HoldLocation").GetComponentInChildren<Rigidbody>();
        Transform ObjectToThrows = GameObject.FindGameObjectWithTag("HoldLocation").GetComponentInChildren<Transform>();
        
        // Select ONLY child of HoldPosition component
        foreach(Transform ObjectToThrow in ObjectToThrows)
        {
            if(ObjectToThrow.gameObject.transform.parent != null)
            {
                ObjectToThrow.transform.SetParent(null);    // Set to main hierachy 
            }
        } 
        
        // Calculate direction
        Vector3 forceDirection = Player.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(Player.position, Player.forward, out hit, 500f))
        {
            forceDirection = (hit.point - AttackPoint.position).normalized;
        }
        
        // Add force to projectile
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        ObjectCollider.enabled = !ObjectCollider.enabled;
        projectileRb.isKinematic = false;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        // totalThrows--;
        
        PlayerInventory.RubbleThrown();

        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
