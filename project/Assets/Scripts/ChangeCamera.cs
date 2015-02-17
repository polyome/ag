using UnityEngine;
using System.Collections;

public class ChangeCamera : MonoBehaviour {	//changed point by keita
	private Camera mainCamera;
	private Camera FPCamera;
	private NavMeshAgent navMeshAg;
	private move3 move3;
	private GUIScript GS;
	private Animator moveSpeed;
	private Movement movement;
	//private CharacterMotor characterMotor;
	//private FPSInputController FPSInputController;

	// Use this for initialization
	void Start () {
		this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		this.FPCamera = GameObject.FindGameObjectWithTag("FPCamera").GetComponent<Camera>();
		this.navMeshAg = GameObject.FindGameObjectWithTag ("Player").GetComponent<NavMeshAgent>();
		this.move3 = GameObject.FindGameObjectWithTag ("Player").GetComponent<move3>();
		/*this.characterMotor = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMotor>();
		this.FPSInputController = GameObject.FindGameObjectWithTag("Player").GetComponent<FPSInputController>();
		this.characterMotor.enabled = false;
		this.FPSInputController.enabled = false;*/
		this.FPCamera.enabled = false;
		this.move3.enabled = false;
		GS = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GUIScript> ();
		this.moveSpeed = GameObject.FindGameObjectWithTag("PlayerModel").GetComponent<Animator> ();
		this.movement = GameObject.FindGameObjectWithTag ("PlayerModel").GetComponent<Movement> ();
	}
	
	// Update is called once per framec
	void Update () {
		//kayaba changed 
		if(GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerThoughts>().inPod) return;

		if(Input.GetKeyDown(KeyCode.C) && !GS.showInventory){
		
			if(this.mainCamera.enabled){
				this.mainCamera.enabled = false;
				this.FPCamera.enabled = true;
				this.navMeshAg.enabled = false;
				this.move3.enabled = true;
				/*this.characterMotor.lastPosition = this.FPCamera.transform.position;
				this.characterMotor.enabled = true;.
				this.FPSInputController.enabled = true;*/

			}else{
				this.mainCamera.enabled = true;
				this.FPCamera.enabled = false;
				this.move3.enabled = false;
				this.navMeshAg.enabled = true;
				movement.closestNavPoint.position = transform.position;
				/*this.characterMotor.enabled = false;
				this.FPSInputController.enabled = false;*/
			}
			this.moveSpeed.SetFloat("Speed",0.0f);
	}
		//kayaba changed e
}
		}
