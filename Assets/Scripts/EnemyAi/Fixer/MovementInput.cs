
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
[RequireComponent(typeof(CharacterController))]
public class MovementInput : MonoBehaviour {

    public float Velocity;
    [Space]

	public float InputX;
	public float InputZ;
	public Vector3 desiredMoveDirection;
	public Vector3 destination;
	public bool blockRotationPlayer;
	public float desiredRotationSpeed = 0.1f;
	public Animator anim;
	public GameObject hatPrefab;
	public float allowPlayerRotation = 0.1f;
	public Camera cam;
	public NavMeshAgent agent;
	public Transform target;
	public Transform hattarget;
	public string TargetLocations;
	public int targetIndex;
	public bool isGrounded;
	public enum state {fix, scared, findhat, pickup,roam};
	public state behaviour;

    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0,1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;
	[Range(2f, 5f)]
	public float speed;
	[Range(0f, 1f)]
	public float bufferDistance=0f;

    public float verticalVel;
    private Vector3 moveVector;

	// Use this for initialization
	void Start () {
		agent=this.GetComponent<NavMeshAgent>();
		anim = this.GetComponent<Animator> ();
		cam = Camera.main;
		targetIndex=Random.Range(0,6);
		TargetLocations="TargetLocation ("+ targetIndex.ToString() +")";
		target=GameObject.Find(TargetLocations).transform;
		behaviour=state.roam;
		anim.SetFloat("Speed",3.5f);
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(behaviour);
		switch(behaviour){
			case state.roam: 
				Move ();
				break;
			case state.findhat: 
				Move ();
				break;
			case state.scared:
				break;
			case state.pickup:
				Pickup ();
				break;
			case state.fix:
				//***********INSERT FIX METHOD***********************//
				break;
		}

        // isGrounded = controller.isGrounded;
        // if (isGrounded)
        // {
        //     verticalVel -= 0;
        // }
        // else
        // {
        //     verticalVel -= 1;
        // }
        // moveVector = new Vector3(0, verticalVel * .2f * Time.deltaTime, 0);
        // controller.Move(moveVector);


    }

	void Move(){
		if(agent.remainingDistance<agent.stoppingDistance+bufferDistance){
			if(target==hattarget){
				target=null;
				anim.SetTrigger("pickup");
				agent.isStopped=true;
				SetBehaviour(state.pickup);
			}
			else if (target.gameObject.tag=="Broken"){
				SetBehaviour(state.fix);
				anim.SetTrigger("fix");
				agent.isStopped=true;
			}
			else {
				FindNewTarget();
			}

		}
		if(target!=null){
			speed=((agent.velocity.magnitude-3.5f)/2.5f);
			destination=target.position;
			agent.destination=destination;
			anim.SetFloat ("Blend", speed, StartAnimTime, Time.deltaTime);
		}
		
	}

	public void SetBehaviour(state newstate){
		behaviour=newstate;
		
	}

	public void FindNewTarget(){
		targetIndex=Random.Range(0,6);
		TargetLocations="TargetLocation ("+ targetIndex.ToString() +")";
		target=GameObject.Find(TargetLocations).transform;
	}
	private void Pickup(){
		if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 >=0.8){
			anim.SetTrigger("normal");
			SetBehaviour(state.roam);
			FindNewTarget();
			agent.isStopped=false;
		}

	}

	void OnTriggerEnter(Collider other) {
		 if (other.gameObject.tag=="Broken" && behaviour==state.roam){
			target=other.transform;
        }
	}

	public void BecomeScared (){
		SetBehaviour(state.scared);
		agent.isStopped=true;
		anim.SetTrigger("dead");
	}

	public void FindHat(){
		SetBehaviour(state.findhat);
		agent.isStopped=false;
	}

}
