
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
		// hatPrefab=Resources.Load("Prefabs/Hat") as GameObject;
		// GameObject ConsHat=Instantiate(hatPrefab);
		//make hat detachtable
		behaviour=state.roam;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(behaviour);
		Move ();

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
				anim.SetTrigger("pickup");
				AnimWait();
				target=null;

			}
			else if (behaviour==state.findhat){
				target=hattarget;
			}
			else {
			targetIndex=Random.Range(0,6);
			TargetLocations="TargetLocation ("+ targetIndex.ToString() +")";
			target=GameObject.Find(TargetLocations).transform;
			}
			
		}
		if(behaviour==state.findhat || behaviour==state.roam &&target!=null	){
			speed=((agent.velocity.magnitude-3.5f)/2.5f);
			destination=target.position;
			agent.destination=destination;
			anim.SetFloat ("Blend", speed, StartAnimTime, Time.deltaTime);
		}
		
	}

	public void SetBehaviour(state newstate){
		behaviour=newstate;
		
	}

	IEnumerator AnimWait(){
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
		anim.SetTrigger("normal");
	}



}
