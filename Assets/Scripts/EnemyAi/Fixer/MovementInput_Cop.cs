using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;




//This script requires you to have setup your animator with 3 parameters, "InputMagnitude", "InputX", "InputZ"
//With a blend tree to control the inputmagnitude and allow blending between animations.
public class MovementInput_Cop : MonoBehaviour {

    [Space]

	public Vector3 destination;
	public float desiredRotationSpeed = 0.1f;
	public Animator anim;
	public NavMeshAgent agent;
	public Transform target;
	public Transform playertarget;
	public string TargetLocations;
	public int targetIndex;
	public bool isGrounded;
	public enum state {roam,chase, arrest};
	public state behaviour;

    [Header("Animation Smoothing")]
    [Range(0,1f)]
    public float StartAnimTime = 0.3f;
	[Range(2f, 5f)]
	public float speed;
	[Range(0f, 1f)]
	private float bufferDistance=3.0f;

    public float verticalVel;
    private Vector3 moveVector;
	private float[] animSpeeds={3.5f, 3.0f};


	// Use this for initialization
	void Start () {
		agent=this.GetComponent<NavMeshAgent>();
		anim = this.GetComponent<Animator> ();
		targetIndex=Random.Range(0,6);
		TargetLocations="TargetLocations ("+ targetIndex.ToString() +")";
		target=GameObject.Find(TargetLocations).transform;
		SetBehaviour(state.roam);
		playertarget=GameObject.FindGameObjectWithTag("Player").transform;
		}
	
	// Update is called once per frame
	void Update () {
		switch(behaviour){
			case state.roam: 
				Move ();
				break;
			case state.chase: 
				Chase ();
				break;
			case state.arrest: 
				Arrest();
				break;
		}
    }

	void Move(){
		if(agent.remainingDistance<agent.stoppingDistance+bufferDistance){
			FindNewTarget();
		}
		if(target!=null){
			speed=((agent.velocity.magnitude-3.5f)/2.5f);
			destination=target.position;
			agent.destination=destination;
			anim.SetFloat ("Blend", speed, StartAnimTime, Time.deltaTime);
		}
	}

	void Chase(){
		if(agent.remainingDistance<agent.stoppingDistance+bufferDistance){
			if(target==playertarget){
				target=null;
				SetBehaviour(state.arrest);
				
			}
		}
		if(target!=null){
			speed=((agent.velocity.magnitude-3.5f)/2.5f)*2;
			destination=target.position;
			agent.destination=destination;
			anim.SetFloat ("Blend", speed, StartAnimTime, Time.deltaTime);
		}
	}
	void Arrest(){

		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().FreezePlayer(transform);
		GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCam>().LockCamera(transform);
		
		
		if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1 >=0.8){
			SetBehaviour(state.roam);
			FindNewTarget();
			
		}
		Invoke("Dead", 3f);
	}
	public void SetBehaviour(state newstate){
		string stateString= newstate.ToString();
		behaviour=newstate;
		anim.SetTrigger(stateString);
		int index =(int)behaviour;
		agent.isStopped=(behaviour==state.arrest);
		if (index<2){
			anim.SetFloat("Speed",animSpeeds[index]);
		}
		
	}

	public void FindNewTarget(){
		targetIndex=Random.Range(0,6);
		TargetLocations="TargetLocations ("+ targetIndex.ToString() +")";
		target=GameObject.Find(TargetLocations).transform;
	}

	void OnTriggerEnter(Collider other) {
		 if (other.gameObject.tag=="Player" && behaviour==state.roam){
			AlertCop();
        }
	}

	public void AlertCop(){
		target=playertarget;
		SetBehaviour(state.chase);
	}
	private void Dead()
	{
		SceneManager.LoadScene("DeathScreen");
	}
}
