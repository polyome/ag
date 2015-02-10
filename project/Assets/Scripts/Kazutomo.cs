using UnityEngine;
using System.Collections;

public class Kazutomo : MonoBehaviour
{
		// Use this for initialization

		Animator animator;
		NavMeshAgent agent;

		void Start ()
		{
		animator = GetComponent<Animator>();
		agent = GameObject.Find ("Character").GetComponent<NavMeshAgent> ();
		}

		// Update is called once per frame
		void Update ()
	{			if (Input.GetKeyDown (KeyCode.LeftShift)) {
						agent.speed = 4.0f;
						animator.SetBool("Run",true);
		}
				else if (Input.GetKeyUp (KeyCode.LeftShift)) {
						agent.speed = 2.0f;
						animator.SetBool("Run",false);
		}
	}
}