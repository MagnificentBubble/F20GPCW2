using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatBehaviour : MonoBehaviour
{
    public Rigidbody hat_rigid;

    private MovementInput parentFixer;
    private Transform parent;

    private Vector3 originPos;

    private Quaternion originRot;

    private GameObject playerHandLoc;

   
    public float radius;

    private bool hatCollect=false;
    private bool thrown=false;
    // Start is called before the first frame update
    void Start()
    {
        hat_rigid=this.GetComponent<Rigidbody>();
        hat_rigid.isKinematic=true;   
        parent=transform.parent;
        parentFixer=parent.GetComponentInParent<MovementInput>();
        parentFixer.hattarget=transform;
        originPos=transform.localPosition;
        originRot=transform.localRotation;

        playerHandLoc = GameObject.FindGameObjectWithTag("HoldLocation");
        Debug.Log(playerHandLoc);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)){Throw();}
        if(hatCollect==false) {CheckForPlayer();}
    }

   public void Throw(){
        parentFixer.BecomeScared();
        this.gameObject.transform.parent=playerHandLoc.transform;   // Change hat to hand of Player
        transform.position = playerHandLoc.transform.position;      // Change hat to bottom right of screen
        hatCollect=true;
        Debug.Log("Thrown");

        // hat_rigid.isKinematic=false;
    }

    void Pickup(Collider other){
        this.gameObject.transform.parent=parent;
        hat_rigid.isKinematic=true;
        transform.localPosition=originPos;
        transform.localRotation=originRot;

    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag=="Fixer" && thrown){
            Pickup(other);
        }
        if (other.gameObject.layer==LayerMask.NameToLayer("whatIsGround")&&parentFixer.behaviour==MovementInput.state.scared){
            thrown=true;
            hatCollect=false;
            parentFixer.target=this.transform;
            parentFixer.FindHat();
        }
        // if (other.gameObject.tag=="Player" && !thrown){
        //     Throw();
        // }
    }

    private void CheckForPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider c in colliders)
        {
            if (c.GetComponent<PlayerMovement>())
            {
                
                //adding picking up hat
                if(Input.GetKey(KeyCode.F))
                {
                    Throw();
                } 
                
            }
        }
    }

}

//Manage animation timings. Character should wait at hat until picked up