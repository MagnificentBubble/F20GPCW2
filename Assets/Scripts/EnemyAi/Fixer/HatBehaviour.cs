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
        parentFixer.SetBehaviour(MovementInput.state.findhat);
        this.gameObject.transform.parent=null;
        hat_rigid.isKinematic=false;
        thrown=true;

    }

    void Pickup(Collider other){
        this.gameObject.transform.parent=GameObject.Find("mixamorig:Head").transform;
        hat_rigid.isKinematic=true;
        transform.localPosition=originPos;
        transform.localRotation=originRot;
        thrown=false;
        parentFixer=transform.GetComponentInParent<MovementInput>();
        Debug.Log("Before Change: "+parentFixer.behaviour);
        Debug.Log(parentFixer.behaviour);
        parentFixer.SetBehaviour(MovementInput.state.roam);
        Debug.Log("After Change: "+parentFixer.behaviour);

    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag=="Fixer" && thrown){
            Pickup(other);
        }
        // if (other.gameObject.tag=="Player" && !thrown){
        //     Throw();
        // }
    }
}

//Manage animation timings. Character should wait at hat until picked up