using UnityEngine;
using System.Collections;

public class TrackingCameraScript : MonoBehaviour {
	public float speed = 0.25f;
	public float zoomNear = 10.0f;
	public float zoomFar = 20.0f;
	float distance;
	Vector3 direction;
	Vector3 newDir;
	GameObject player;
	Rect trackBox;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("PlayerModel");
		trackBox = new Rect (Screen.width/3,Screen.height/3,Screen.width/3,Screen.height/3);
	
	}
	
	// Update is called once per frame
	void Update () {

		//rotate
		if(!trackBox.Contains(camera.WorldToScreenPoint(player.transform.position))){
			direction = player.transform.position - transform.position;
			newDir = Vector3.RotateTowards(transform.forward, direction,speed * Time.deltaTime,0.0f);
			transform.rotation = Quaternion.LookRotation(newDir);
		}

		//zoom
		//Debug.Log(Vector3.Distance(transform.position,player.transform.position).ToString());
		distance = Vector3.Distance (transform.position, player.transform.position);
		/*if(distance<zoomNear){
			camera.fieldOfView = 60;
		}
		else{
			if(distance>=zoomFar){
				camera.fieldOfView = 20;
			}
			else{
				camera.fieldOfView = 40;
			}
		}*/

	}
}
