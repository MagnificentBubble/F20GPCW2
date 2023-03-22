using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    /* private PlayerInventory PlayerInv; */

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

    bool readyToThrow;

    private void Start()
        {
            readyToThrow = true;
            /* PlayerInv = GameObject.FindObjectOfType<PlayerInventory>().GetComponent<PlayerInventory>(); */

        }

    private void Update()
    {
        /* totalThrows = PlayerInv.NumberOfTrash; // total throws are set to the number of trash collected */



        if((Input.GetKeyDown(throwKey) == true) && (readyToThrow == true))
        {
            Throw();
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
        
        
        /* PlayerInv.TrashThrown(); */

        // implement throwCooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

}
