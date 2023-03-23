using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatBehaviour : MonoBehaviour
{
    public Rigidbody hat_rigid;

    private bool thrown=false;
    // Start is called before the first frame update
    void Start()
    {
        hat_rigid=this.GetComponent<Rigidbody>();
        hat_rigid.isKinematic=true;   
    }

    // Update is called once per frame
    void Update()
    {
    if (Input.GetKeyDown(KeyCode.Alpha0)){Throw();}
    }

    void Throw(){
        this.gameObject.transform.Find("Jammo_Player").GetComponent<MovementInput>().behaviour=MovementInput.state.scared;
        this.gameObject.transform.parent=null;
        hat_rigid.isKinematic=false;
        thrown=true;
    }

    void Pickup(Collider other){
        this.gameObject.transform.parent=other.transform.Find("mixamorig:Head");
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
