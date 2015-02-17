using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
	public GameObject[] credits;
	public Vector3[] cameraPositions;
	public GameObject mainMenu;
	public Camera camera;
	Vector3 startPosition;
	Quaternion startRotation;

	static int flag = 0;

	// Use this for initialization
	void Start () {

	}

	void Awake(){

	}

	public void RollCredits(){
		Debug.Log ("credits rolling");
		startPosition = new Vector3 (18.36255f,17.29464f,18.70562f);
		startRotation = Quaternion.identity;
		startRotation.eulerAngles = new Vector3 (359.7f,42.7f,358.7f);
		cameraPositions = new Vector3[credits.Length];
		for(int i=0;i<cameraPositions.Length;i++){
			cameraPositions[i]=credits[i].transform.Find("cameraPosition").transform.position;
			credits[i].SetActive(false);
		}
		StartCoroutine (nextCredit(0));
	}

	IEnumerator nextCredit(int creditIndex){
		credits[creditIndex].SetActive (true);
		camera.transform.position = cameraPositions [creditIndex];
		camera.transform.rotation = credits [creditIndex].transform.rotation;
		//camera.transform.LookAt (credits[creditIndex].transform.position);
		yield return(new WaitForSeconds(5f));
		credits[creditIndex].SetActive (false);
		if(creditIndex==credits.Length-1){
			if(Application.loadedLevelName=="credit"){
				Application.LoadLevel("main menu");
			}
			camera.transform.position=startPosition;
			camera.transform.rotation=startRotation;
			StopAllCoroutines();
			mainMenu.SetActive(true);
			gameObject.SetActive(false);
		}else{
			StartCoroutine(nextCredit(creditIndex+1));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			camera.transform.position=startPosition;
			camera.transform.rotation=startRotation;
			StopAllCoroutines();
			mainMenu.SetActive(true);
			gameObject.SetActive(false);
		}
	}
}
