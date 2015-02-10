using UnityEngine;
using System.Collections;
public class Movement : MonoBehaviour {

	Animator animator;
	Vector3 targetPos;
	Vector3 currentPos;
	//public float speed = 0.01f;
	float distance;
	public bool turning = false;
	Vector3 newRot;
	bool moving;
	bool showOptionsAfterMoving;
	RaycastHit hit;
	NavMeshAgent agent;
	NavMeshHit closestNavPoint;
	GameObject canvasControl;
	GameObject item;

	void Start () {
		currentPos = transform.position;
		animator = GetComponent<Animator>();
		targetPos = currentPos;
		moving = false;
		agent = GameObject.Find ("Character").GetComponent<NavMeshAgent> ();
		canvasControl = GameObject.FindGameObjectWithTag ("canvasControl");
		showOptionsAfterMoving = false;
	}
	public void Move(Vector3 targetPos){
		//Debug.Log ("target: x="+hit.point.x.ToString()+" y="+hit.point.y.ToString()+" z="+hit.point.z.ToString());
		NavMesh.SamplePosition (targetPos,out closestNavPoint,1000,1);
		//Debug.Log ("raycast hit x:"+targetPos.x.ToString()+" y:"+targetPos.y.ToString()+" z:"+targetPos.z.ToString());
		//Debug.Log ("closest nav x:"+closestNavPoint.position.x.ToString()+" y:"+closestNavPoint.position.y.ToString()+" z:"+closestNavPoint.position.z.ToString());
		GameObject.Find("Character").transform.LookAt (closestNavPoint.position);
		agent.SetDestination (closestNavPoint.position);
		animator.SetFloat ("Speed", 2);
		/*
		moving = true;
		*/
		//Turn ();
	}

	public void Move(GameObject target){
		item = target;
		showOptionsAfterMoving = true;
		Vector3 direction =(target.transform.position-transform.position).normalized;
		Vector3 newTarget = target.transform.position-1.5f*direction;
		NavMesh.SamplePosition (newTarget,out closestNavPoint,1000,1);
		GameObject.Find("Character").transform.LookAt (closestNavPoint.position);
		agent.SetDestination (closestNavPoint.position);
		animator.SetFloat ("Speed", 1);
	}

	void Turn() {
		//newRot = Quaternion.LookRotation(targetPos - transform.localRotation.eulerAngles).eulerAngles;
		//newRot = Quaternion.LookRotation (targetPos).eulerAngles;
		//newRot = Quaternion.FromToRotation (transform.position, targetPos).eulerAngles;
		newRot.y = Vector3.Angle (transform.forward, targetPos);
		newRot.x = 0;
		newRot.z = 0;
		Vector3 cross = Vector3.Cross (transform.forward, targetPos);
		if(cross.y < 0) newRot.y = -newRot.y;
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRot), Time.deltaTime*10f);
		//Debug.Log(cross.y);
		Debug.Log(newRot.y);
		//turning = true;
		//animator.SetFloat ("Direction", newRot.y);
		//animator.SetBool ("Turn", true);
		//StartCoroutine(TurnReset ());
	}
	IEnumerator TurnReset() {
		yield return new WaitForSeconds (0.2f);
		//yield return new WaitForFixedUpdate ();
		animator.SetBool ("Turn", false);
		moving = true;
	}
	void Update () {
		if(Vector3.Distance(transform.position,closestNavPoint.position)<1.0f){
			animator.SetFloat ("Speed", 0);
		}

		if(showOptionsAfterMoving){
			if(agent.remainingDistance<0.1f){
				canvasControl.GetComponent<CanvasControllerGV>().ShowItemOptions(item);
				showOptionsAfterMoving = false;
			}
		}

		/*
		currentPos = transform.position;
		//targetPos = currentPos;
		if (moving) {
			//targetPos = new Vector3(hit.point.x, 0, hit.point.z);
			//turning = true;
		}
		*/
		/*if(animator.GetBool("Turn")&&!animator.IsInTransition(0)){
		animator.MatchTarget (transform.position,
		Quaternion.Euler(newRot),
		AvatarTarget.Root,
		new MatchTargetWeightMask (Vector3.zero, 1f),
		0.15f,
		0.90f);
		//newRot = transform.rotation.eulerAngles;
		//animator.SetFloat("Direction", 0);
		//animator.SetBool("Turn", false);
		//TurnReset();
		}*/
				/*if(turning){
		newRot = Quaternion.LookRotation(targetPos - transform.position).eulerAngles;
		newRot.x = 0;
		newRot.z = 0;
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRot), Time.deltaTime*10f);
		Debug.Log(newRot.y);
		animator.SetFloat("Direction", newRot.y);
		if(Quaternion.Angle(transform.rotation, Quaternion.Euler(newRot)) <= 3){
		turning = false;
		//newRot = transform.position;
		}
		}
		else {
		newRot = transform.position;
		}*/

		/*
		if(Vector3.Distance(currentPos, targetPos)>=1 && moving){
			if(Vector3.Distance(currentPos, targetPos)>=5)animator.SetFloat("Speed", 5);
			if(Vector3.Distance(currentPos, targetPos)<5) animator.SetFloat("Speed", 1);
			animator.SetFloat("Distance", Vector3.Distance(currentPos, targetPos));
		}
		else{
			animator.SetFloat("Speed", 0);
			targetPos = currentPos;
			turning = false;
			moving = false;
		}
		*/
	}
}