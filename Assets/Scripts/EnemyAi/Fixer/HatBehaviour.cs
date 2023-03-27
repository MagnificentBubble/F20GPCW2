using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatBehaviour : MonoBehaviour
{
    public Rigidbody hat_rigid;

    private MovementInput parentFixer;

    private Vector3 originPos;

    private Quaternion originRot;

    private bool thrown=false;
    // Start is called before the first frame update
    void Start()
    {
        hat_rigid=this.GetComponent<Rigidbody>();
        hat_rigid.isKinematic=true;   
        parentFixer=transform.GetComponentInParent<MovementInput>();
        parentFixer.hattarget=transform;
        originPos=transform.localPosition;
        originRot=transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){Throw();}
    }

    void Throw(){
        parentFixer.BecomeScared();
        this.gameObject.transform.parent=null;//*********Change to hand of Player********//
        hat_rigid.isKinematic=false;
        thrown=true;

    }

    void Pickup(Collider other){
        this.gameObject.transform.parent=GameObject.Find("mixamorig:Head").transform;
        hat_rigid.isKinematic=true;
        transform.localPosition=originPos;
        transform.localRotation=originRot;
        thrown=false;
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag=="Fixer" && thrown){
            Pickup(other);
        }
        if (other.gameObject.layer==LayerMask.NameToLayer("whatIsGround")&&parentFixer.behaviour==MovementInput.state.scared){
            Debug.Log("HatGrounded");
            parentFixer.target=this.transform;
            parentFixer.FindHat();
        }
        // if (other.gameObject.tag=="Player" && !thrown){
        //     Throw();
        // }
    }
}

//Manage animation timings. Character should wait at hat until picked up