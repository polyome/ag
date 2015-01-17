using UnityEngine;
using System.Collections;

public class RotatingMainMenu : MonoBehaviour {
	public GameObject ship;
	public float rotationSpeed=1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (ship.transform.position,Vector3.up,Time.deltaTime*rotationSpeed);
	}
}
