using UnityEngine;
using System.Collections;

public class move3 : MonoBehaviour {
	
	private float walkSpeed =   5.0f;
	private float runSpeed = 8.0f;
	//private float jumpspeed = 5.0f;
	private float gravity = 20.0f;
	
	
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	
	public void Start()
	{
		controller = GetComponent<CharacterController>();
	}
	
	public void Update()
	{
		float speed = this.walkSpeed;
		if (controller.isGrounded)
		{
			if(Input.GetKeyDown(KeyCode.F) == true) this.transform.Rotate(new Vector3(0,180,0));
			if(Input.GetKey(KeyCode.LeftShift) == true) speed = this.runSpeed;
			moveDirection = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxisRaw("Vertical"));
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= speed;
			
			/*if(Input.GetButton ( "Jump"))
			{
				moveDirection.y = jumpspeed;
			}*/
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move (moveDirection * Time.deltaTime);
	}
}