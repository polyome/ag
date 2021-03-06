﻿using UnityEngine;
using System.Collections;

public class RoomTriggerer : MonoBehaviour {

	public int roomID;
	private Vector3 offset;
	private Quaternion direction;

	private GameObject camera;
	private GameObject FPCamera;
	GameObject player;
	//private GameObject[] rooms;

	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
	//	FPCamera = GameObject.FindGameObjectWithTag ("FPCamera");
		player = GameObject.FindGameObjectWithTag ("Player");
		//offset =  camera.transform.position;

		/*
		rooms = new GameObject[4];

		rooms[0] = GameObject.FindGameObjectWithTag ("Room1");
		rooms[1] = GameObject.FindGameObjectWithTag ("Room2");
		rooms[2] = GameObject.FindGameObjectWithTag ("Room3");
		rooms[3] = GameObject.FindGameObjectWithTag ("Room4");
		*/

	}
	
	void OnTriggerEnter(Collider other) {
		if(other.tag == "PlayerModel" ){
			Transform cameraPosition= transform.Find("CameraPosition");
			offset = cameraPosition.position;
			direction = cameraPosition.rotation;
	//		if(FPCamera.GetComponent<Camera>().enabled == false){
			camera.transform.position = offset;
			camera.transform.rotation = direction;
			camera.GetComponent<Camera>().fieldOfView = GetComponentInChildren<Camera>().fieldOfView;
			camera.transform.LookAt(player.transform.position);
				}
		}
	//}
}
