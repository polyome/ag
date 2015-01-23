using UnityEngine;
using System.Collections;

public class Kazutomo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
						GetComponent<NavMeshAgent> ().speed = 8.0f;
				}
		if (Input.GetKeyUp (KeyCode.Space)) {
						GetComponent<NavMeshAgent> ().speed = 3.5f;
				}
	}
}
