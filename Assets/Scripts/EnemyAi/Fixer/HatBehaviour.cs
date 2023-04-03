using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatBehaviour : MonoBehaviour
{
    public bool closestToPlayer;
    // Animator anim;
    public Rigidbody hat_rigid;

    private MovementInput parentFixer;
    private Transform parent;

    private Vector3 originPos;

    private Quaternion originRot;

    private GameObject playerHandLoc;
   
    public float radius;

    [HideInInspector]
    public bool hatCollect=false; 
    private bool thrown=false;
    // Start is called before the first frame update
    void Start()
    {
        radius = 50;
        hat_rigid=this.GetComponent<Rigidbody>();
        hat_rigid.isKinematic=true;   
        parent=transform.parent;
        parentFixer=parent.GetComponentInParent<MovementInput>();
        parentFixer.hattarget=transform;
        originPos=transform.localPosition;
        originRot=transform.localRotation;

        // anim = GetComponent<Animator>();

        playerHandLoc = GameObject.FindGameObjectWithTag("HoldLocation");
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(hat_rigid.velocity.sqrMagnitude<=0.1);
        //if (Input.GetKeyDown(KeyCode.Space)){Throw();}
        //if(hatCollect==false) {CheckForPlayer();}

        // HatAnim();
        // Debug.Log(PlayerInventory.childExists);
    }

    // Hat picked by player from fixer's head
   public void Throw(){
        hat_rigid.isKinematic = true;
        parentFixer.BecomeScared();
        this.gameObject.transform.parent=playerHandLoc.transform;   // Change to hand of Player
        transform.position = playerHandLoc.transform.position;      // Change to HoldLocation parent
        hatCollect=true;
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
        if (other.gameObject.layer==LayerMask.NameToLayer("whatIsGround")&& parentFixer.behaviour==MovementInput.state.scared){
            thrown=true;
            hatCollect=false;
            parentFixer.target=this.transform;
            parentFixer.FindHat();
        }
        // if (other.gameObject.tag=="Player" && !thrown){
        //     Throw();
        // }
    }

    // private void CheckForPlayer()
    // {
    //     Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
    //     foreach(Collider c in colliders)
    //     {
    //         if (c.GetComponent<PlayerMovement>())
    //         {
                
    //             //adding picking up hat

                
    //         }
    //     }
    // }

    public void SetClosestToPlayer()
    {
        closestToPlayer = true;
        Invoke(nameof(_SetClosestToPlayer),1f);
    }

    public void _SetClosestToPlayer()
    {
        closestToPlayer = false;
    }

    // private void HatAnim()
    // {
    //     if (Input.GetKey(KeyCode.L))
    //     {
    //         anim.SetBool("Attack", true);
    //     }
    //     // else if (Input.GetKeyUp(KeyCode.Mouse0))
    //     // {
    //     //     anim.SetBool("Attack",false);
    //     // }
    //     else{anim.SetBool("Attack",false);}
    // }

}

//Manage animation timings. Character should wait at hat until picked up