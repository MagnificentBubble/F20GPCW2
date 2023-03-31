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
    public GameObject ObjectToThrow;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    public KeyCode throwKey = KeyCode.Mouse1;
    public float throwForce;
    public float throwUpwardForce;

    [Header("Melee")]
    public KeyCode MeleeKey = KeyCode.Mouse0;
    private Animator Animator;
    private GameObject MeleeWeapon;
    

    bool readyToThrow;

    private void Start()
        {
            readyToThrow = true;
            PlayerInventory = GetComponent<PlayerInventory>();
            Animator = GetComponent<Animator>();
            MeleeWeapon = GameObject.FindGameObjectWithTag("MeleeWeapon");
        }

    private void Update()
    {
        // Throw when number of rubble is not zero
        if((Input.GetKeyDown(throwKey) == true) && (readyToThrow == true) && (PlayerInventory.NumberOfRubble > 0))
        {
            Throw();
        }

        // Melee when number of rubble is not zero
        if(Input.GetKeyDown(MeleeKey) == true && (PlayerInventory.NumberOfRubble > 0))
        {
            Animator.SetTrigger("isMelee");
        }

        // Remove rubble on hand when number of rubble is zero
        if(PlayerInventory.NumberOfRubble < 1)
        {
            MeleeWeapon.SetActive(false);
        }
    }

    private void Throw()
    {
        
        readyToThrow = false;

        // instantiate object to throw
        GameObject projectile = Instantiate(ObjectToThrow, AttackPoint.position, Player.rotation);

        // get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate direction
        Vector3 forceDirection = Player.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(Player.position, Player.forward, out hit, 500f))
        {
            forceDirection = (hit.point - AttackPoint.position).normalized;
        }

        // add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;
        
        PlayerInventory.RubbleThrown();

        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

}
