using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {

	public bool rotating;
	public bool pickable;
	public bool usable;
	public bool showItemMenu;
	public AudioClip useSound;
	public AudioClip examineSound;
	float rotateSpeed = 70f;
	GameObject manager;
	GameObject player;
	public string examineDescription;
	public Shader highlight;
	Shader current;
	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag ("GameController");
		player = GameObject.FindGameObjectWithTag("Player");
		showItemMenu = false;
		current = renderer.material.shader;
		//rotating = false;
	}
	// Update is called once per frame
	void Update () {
		if (rotating) {
			transform.RotateAround(transform.position,transform.forward,rotateSpeed/3 * Time.deltaTime);
			transform.RotateAround(transform.position,transform.up,rotateSpeed/2 * Time.deltaTime);
			transform.RotateAround(transform.position,transform.right,rotateSpeed/4 * Time.deltaTime);
		}
	}
	void OnMouseOver()
	{
		if(!manager.GetComponent<GUIScript>().showLookAt){
			if(!manager.GetComponent<GUIScript>().showExamine){
				renderer.material.shader = highlight;
				//renderer.material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
			}
		}
		else{
			if(usable){
				renderer.material.shader = highlight;
				//renderer.material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
				manager.GetComponent<GUIScript>().showItemTooltip = true;
				if(Input.GetMouseButtonDown(0)){
					switch(transform.name){
					case "ElevatorButton1":
						//player.transform.position = manager.GetComponent<InventoryScript>().rooms[0].transform.position + new Vector3(0,1,0);
						player.GetComponent<NavMeshAgent>().Warp(manager.GetComponent<InventoryScript>().rooms[0].transform.position);
						manager.GetComponent<GUIScript>().examineCamera.enabled = false;
						manager.GetComponent<GUIScript>().showLookAt = false;
						manager.GetComponent<GUIScript>().showHelp = false;
						break;
					case "ElevatorButton2":
						//player.transform.position = manager.GetComponent<InventoryScript>().rooms[1].transform.position + new Vector3(0,1,0);
						player.GetComponent<NavMeshAgent>().Warp(manager.GetComponent<InventoryScript>().rooms[1].transform.position);
						manager.GetComponent<GUIScript>().examineCamera.enabled = false;
						manager.GetComponent<GUIScript>().showLookAt = false;
						manager.GetComponent<GUIScript>().showHelp = false;
						break;
					case "ElevatorButton3":
						//player.transform.position = manager.GetComponent<InventoryScript>().rooms[2].transform.position + new Vector3(0,1,0);
						player.GetComponent<NavMeshAgent>().Warp(manager.GetComponent<InventoryScript>().rooms[2].transform.position);
						manager.GetComponent<GUIScript>().examineCamera.enabled = false;
						manager.GetComponent<GUIScript>().showLookAt = false;
						manager.GetComponent<GUIScript>().showHelp = false;
						break;
					case "ElevatorButton4":
						//player.transform.position = manager.GetComponent<InventoryScript>().rooms[3].transform.position + new Vector3(0,1,0);
						player.GetComponent<NavMeshAgent>().Warp(manager.GetComponent<InventoryScript>().rooms[3].transform.position);
						manager.GetComponent<GUIScript>().examineCamera.enabled = false;
						manager.GetComponent<GUIScript>().showLookAt = false;
						manager.GetComponent<GUIScript>().showHelp = false;
						break;
					default:
						break;
					}
					//Debug.Log ("pressed "+transform.name);
				}
			}
		}
		/*
		if(!GameObject.Find("Manager").GetComponent<GUIScript>().showExamine && !GameObject.Find("Manager").GetComponent<GUIScript>().showLookAt){
			renderer.material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
		}
		*/
	}
	void OnMouseExit()
	{
		//renderer.material.shader = Shader.Find("Diffuse");
		renderer.material.shader = current;
		manager.GetComponent<GUIScript> ().showItemTooltip = false;
	}
}