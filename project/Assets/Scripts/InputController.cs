using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class InputController : MonoBehaviour {
	//public float cameraSpeed = 20.0f;
	public float pickupRadius = 2.0f;
	public GameObject player;
	private Camera FPCamera;	//kayaba changed
	GameObject canvasControl;
	GameObject itemOptions;
	EventSystem eventSystem;
	bool dragging;
	Vector2 oldPosition;
	float rotationSpeed = 10.0f;
	int itemNumber;
	//Rect inventoryButton;
	//bool disableRay = false;

	// Use this for initialization
	void Start () {
		//inventoryButton = new Rect (Screen.width-100,0,100,100);
		player = GameObject.FindGameObjectWithTag ("PlayerModel");
		canvasControl = GameObject.FindGameObjectWithTag ("canvasControl");
		eventSystem = GameObject.FindGameObjectWithTag ("EventSystem").GetComponent<EventSystem>();
		itemOptions = GameObject.FindGameObjectWithTag ("ItemOptions");
		dragging = false;
		this.FPCamera = GameObject.FindGameObjectWithTag ("FPCamera").GetComponent<Camera> ();	//kayaba changed
	}

	//stuff to do when user clicked left mouse button
	void LeftClick(){
		Ray ray;
		if (this.FPCamera.enabled == false)	//kayaba changed
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		else
			ray = this.FPCamera.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast (ray, out hit, 10000);
		Vector3 targetPos;
		
		if (this.FPCamera.enabled == false) {	//kayaba changed
			this.pickupRadius = 2.0f;
			switch (hit.transform.tag) {
			case "item":
				Debug.Log ("show item option_2");
				if (Vector3.Distance (GameObject.FindGameObjectWithTag ("Player").transform.position, hit.transform.position) < pickupRadius) {
					canvasControl.GetComponent<CanvasControllerGV> ().ShowItemOptions (hit.collider.gameObject);
					
				} else {
					if (player.transform.parent.GetComponent<PlayerThoughts> ().inPod == false) {
						player.GetComponent<Movement> ().Move (hit.collider.gameObject);
					} else {
						GetComponent<GUIScript> ().PlayerThoughts ("I need to get out of this pod first.");
					}
				}
				break;
			case "locker":
				if (Vector3.Distance (GameObject.FindGameObjectWithTag ("Player").transform.position, hit.transform.position) < pickupRadius) {
					canvasControl.GetComponent<CanvasControllerGV> ().ShowItemOptions (hit.collider.gameObject);
					
				} else {
					player.GetComponent<Movement> ().Move (hit.collider.gameObject);
				}
				break;
			case "door":
				if (Vector3.Distance (GameObject.FindGameObjectWithTag ("Player").transform.position, hit.transform.position) < pickupRadius) {
					canvasControl.GetComponent<CanvasControllerGV> ().ShowItemOptions (hit.collider.gameObject);
					
				} else {
					player.GetComponent<Movement> ().Move (hit.collider.gameObject);
				}
				break;
			case "map":
				if (Vector3.Distance (GameObject.FindGameObjectWithTag ("Player").transform.position, hit.transform.position) < pickupRadius + 1) {
					GetComponent<GUIScript> ().ShowContextMenu (hit);
				}
				break;
			case "NPC":
				switch (hit.collider.name) {
				case "TestNPC":
					hit.collider.GetComponent<TestNPCScript> ().Talk ();
					GetComponent<GUIScript> ().talkingNPC = hit.collider.gameObject;
					break;
				default:
					break;
				}
				break;
			default :
				if (player.transform.parent.GetComponent<PlayerThoughts> ().inPod == false) { //kayaba changed
					targetPos = new Vector3 (hit.point.x, hit.point.y, hit.point.z);
					player.GetComponent<Movement> ().Move (targetPos);
					if (canvasControl.GetComponent<CanvasControllerGV> ().showItemOptions) {
						canvasControl.GetComponent<CanvasControllerGV> ().HideItemOptions ();
					}
				}
				break;
			}
		} else {
			this.pickupRadius = 3.0f;
			switch (hit.transform.tag) {
			case "item":
				Debug.Log ("show item option_2");
				if (Vector3.Distance (GameObject.FindGameObjectWithTag ("Player").transform.position, hit.transform.position) < pickupRadius) {
					canvasControl.GetComponent<CanvasControllerGV> ().ShowItemOptions (hit.collider.gameObject);
					
				} else {
					if (player.transform.parent.GetComponent<PlayerThoughts> ().inPod == false) {
						//player.GetComponent<Movement> ().Move (hit.collider.gameObject);
					} else {
						GetComponent<GUIScript> ().PlayerThoughts ("I need to get out of this pod first.");
					}
				}
				break;
			case "locker":
				if (Vector3.Distance (GameObject.FindGameObjectWithTag ("Player").transform.position, hit.transform.position) < pickupRadius) {
					canvasControl.GetComponent<CanvasControllerGV> ().ShowItemOptions (hit.collider.gameObject);
					
				} else {
					//player.GetComponent<Movement> ().Move (hit.collider.gameObject);
				}
				break;
			case "door":
				if (Vector3.Distance (GameObject.FindGameObjectWithTag ("Player").transform.position, hit.transform.position) < pickupRadius) {
					canvasControl.GetComponent<CanvasControllerGV> ().ShowItemOptions (hit.collider.gameObject);
					
				} else {
					//player.GetComponent<Movement> ().Move (hit.collider.gameObject);
				}
				break;
			case "map":
				if (Vector3.Distance (GameObject.FindGameObjectWithTag ("Player").transform.position, hit.transform.position) < pickupRadius + 1) {
					GetComponent<GUIScript> ().ShowContextMenu (hit);
				}
				break;
			case "NPC":
				switch (hit.collider.name) {
				case "TestNPC":
					hit.collider.GetComponent<TestNPCScript> ().Talk ();
					GetComponent<GUIScript> ().talkingNPC = hit.collider.gameObject;
					break;
				default:
					break;
				}
				break;
			default :
				if (player.transform.parent.GetComponent<PlayerThoughts> ().inPod == false) { //kayaba changed
					//targetPos = new Vector3 (hit.point.x, hit.point.y, hit.point.z);
					//player.GetComponent<Movement> ().Move (targetPos);
					if (canvasControl.GetComponent<CanvasControllerGV> ().showItemOptions) {
						canvasControl.GetComponent<CanvasControllerGV> ().HideItemOptions ();
					}
				}
				break;
			}
		}
	}

	//close examine view
	public void CloseExamine(){
		if(GetComponent<GUIScript>().showExamine){
			int index = canvasControl.GetComponent<CanvasControllerGV>().itemNumber;
			GetComponent<InventoryScript>().GetInventory()[index].GetComponent<ItemScript>().rotating = true;
		}
		GetComponent<GUIScript>().showExamine =false;
		GetComponent<GUIScript>().showHelp =false;
		GetComponent<GUIScript>().inventorySelect.enabled = false;
	}

	IEnumerator WaitForPod(float time){
		yield return(new WaitForSeconds(time));
		player.transform.parent.GetComponent<PlayerThoughts>().inPod=false;
		player.transform.parent.GetComponent<NavMeshAgent>().enabled=true;
		player.GetComponent<CapsuleCollider>().enabled=true;
		player.GetComponent<Rigidbody>().useGravity = true;
		player.GetComponent<Rigidbody>().constraints &= RigidbodyConstraints.FreezeAll;
		player.transform.parent.transform.rotation = Quaternion.Euler( new Vector3 (0,0,0));
		//player.transform.localPosition=new Vector3(0,0.2f,0);
	}


	void UseOn(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Physics.Raycast(ray, out hit, 10000);
		GameObject item = GetComponent<InventoryScript> ().GetInventory () [itemNumber];
		Debug.Log("used "+item.ToString()+" on "+hit.transform.name);
		//GetComponent<GUIScript>().PlayerThoughts("Don't be silly!");
		switch(hit.transform.tag){
		case "item":
			switch(hit.transform.name){
			case "stasispod_player":
				switch(item.name){
				case "bandages":
					hit.transform.animation.Play();
					GetComponent<InventoryScript>().RemoveItem(item);
					StartCoroutine(WaitForPod(4f));
					break;
				default:
					GetComponent<GUIScript>().PlayerThoughts("That would be silly.");
					break;
				}
				break;
			default:
				GetComponent<GUIScript>().PlayerThoughts("How's that supposed to work?");
				break;
			}
			break;
		default:
			GetComponent<GUIScript>().PlayerThoughts("It doesn't seem to work that way.");
			break;
		}
		Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
		canvasControl.GetComponent<CanvasControllerGV>().targetingUse=false;
	}

	// Update is called once per frame
	void Update () {

		//is game view active?
		if(canvasControl.GetComponent<CanvasControllerGV>().GameViewCanvas.activeInHierarchy){
			//controls active in game view
			if(!eventSystem.IsPointerOverGameObject()){
				if(Input.GetMouseButtonDown(0)){
					LeftClick();
				}
			}
		} else{ 
			//is inventory view active?
			if(canvasControl.GetComponent<CanvasControllerGV>().InventoryCanvas.activeInHierarchy){
				//controls active in inventory
				if(canvasControl.GetComponent<CanvasControllerGV>().targetingUse){
					//use object on target
					if(Input.GetMouseButtonDown(0)){
						UseOn();
					}
					//cancel
					if(Input.GetMouseButtonDown(1)){
						Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
						canvasControl.GetComponent<CanvasControllerGV>().targetingUse=false;
					}
				}else{
					for(int i=0;i<10;i++){
						if(GetComponent<GUIScript>().itemCameras[i].pixelRect.Contains(Input.mousePosition)){
							if(!GetComponent<GUIScript>().showExamine){
								GetComponent<GUIScript>().inventorySelect.pixelInset = GetComponent<GUIScript>().itemCameras[i].pixelRect;
							}
							GetComponent<GUIScript>().inventorySelect.enabled = true;
							if (Input.GetMouseButtonDown(0)) {
								if(!GetComponent<GUIScript>().showExamine){
									if(i<GetComponent<InventoryScript>().GetInventory().Count){
										//GetComponent<GUIScript>().ShowContextMenu(i);
										itemNumber=i;
										canvasControl.GetComponent<CanvasControllerGV>().ShowItemOptions(i);
									}
								}
							}
						}
					}
				}

				//controls for examine
				if(GetComponent<GUIScript>().showExamine){
					if(Input.GetMouseButtonDown(0)){
						oldPosition = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
						dragging = true;
					}
					if(Input.GetMouseButtonUp(0)){
						dragging = false;
					}
					if(dragging){
						Vector2 dragDelta = new Vector2(Input.mousePosition.x - oldPosition.x,Input.mousePosition.y - oldPosition.y);
						int index = canvasControl.GetComponent<CanvasControllerGV>().itemNumber;
						GetComponent<InventoryScript>().GetInventory()[index].transform.RotateAround(GetComponent<InventoryScript>().GetInventory()[index].transform.position,-Vector3.up,Time.deltaTime*dragDelta.x*rotationSpeed);
						GetComponent<InventoryScript>().GetInventory()[index].transform.RotateAround(GetComponent<InventoryScript>().GetInventory()[index].transform.position,Vector3.right,Time.deltaTime*dragDelta.y*rotationSpeed);
						oldPosition = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
					}
					if(Input.GetMouseButtonDown(1)){
						CloseExamine();
					}
				}

			} else{
				//is PDA active?
				if(canvasControl.GetComponent<CanvasControllerGV>().PDACanvas.activeInHierarchy){
					//controls active in PDA
				} else{
					//is game paused?
					if(canvasControl.GetComponent<CanvasControllerGV>().PauseCanvas.activeInHierarchy){
						//controls active when game is paused
					} else{
						//is there dialogue?

					}
				}
			}
		}


		/*
		if(GetComponent<GUIScript>().showLookAt){
			disableRay = true;
			if(Input.GetMouseButtonDown(1)){
				GetComponent<GUIScript>().showLookAt = false;
				GetComponent<GUIScript>().examineCamera.enabled = false;
				GetComponent<GUIScript>().showHelp = false;
			}
			int borderWidth = 50;
			Rect topArea = new Rect(0,Screen.height-borderWidth,Screen.width,borderWidth);
			if(topArea.Contains(Input.mousePosition)){
				GetComponent<GUIScript>().examineCamera.transform.eulerAngles += new Vector3(-cameraSpeed*Time.deltaTime,0,0);
			}
			Rect bottomArea = new Rect(0,0,Screen.width,borderWidth);
			if(bottomArea.Contains(Input.mousePosition)){
				GetComponent<GUIScript>().examineCamera.transform.eulerAngles += new Vector3(cameraSpeed*Time.deltaTime,0,0);
			}
			Rect leftArea = new Rect(0,0,borderWidth,Screen.height);
			if(leftArea.Contains(Input.mousePosition)){
				GetComponent<GUIScript>().examineCamera.transform.eulerAngles += new Vector3(0,-cameraSpeed*Time.deltaTime,0);
			}
			Rect rightArea = new Rect(Screen.width-borderWidth,0,borderWidth,Screen.height);
			if(rightArea.Contains(Input.mousePosition)){
				GetComponent<GUIScript>().examineCamera.transform.eulerAngles += new Vector3(0,cameraSpeed*Time.deltaTime,0);
			}

			/*
			if(Input.GetMouseButtonDown(0)){
				oldPosition = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
				dragging = true;
			}
			if(Input.GetMouseButtonUp(0)){
				dragging = false;
			}
			if(dragging){
				Vector2 dragDelta = new Vector2(Input.mousePosition.x - oldPosition.x,Input.mousePosition.y - oldPosition.y);
				//GetComponent<GUIScript>().examineCamera.transform.Rotate(new Vector3(-cameraSpeed*dragDelta.y*Time.deltaTime,cameraSpeed*dragDelta.x*Time.deltaTime,0));
				GetComponent<GUIScript>().examineCamera.transform.eulerAngles = GetComponent<GUIScript>().examineCamera.transform.eulerAngles + new Vector3(-cameraSpeed*dragDelta.y*Time.deltaTime,cameraSpeed*dragDelta.x*Time.deltaTime,0);
				oldPosition = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
			}
			*/
		/*
		}

		else{
			if(GetComponent<GUIScript>().showDialogue){
				if(Input.GetMouseButtonDown(0)){
					int lenght = GetComponent<GUIScript>().choices.Length;
					if(lenght>0){
						if(GetComponent<GUIScript>().displayedChoices[lenght-1].Equals(GetComponent<GUIScript>().choices[lenght-1])){
							//GetComponent<GUIScript>().CloseDialogue();
						}
						else{
							GetComponent<GUIScript>().StopAllCoroutines();
							GetComponent<GUIScript>().displayedText = GetComponent<GUIScript>().text;
							for(int i=0;i<GetComponent<GUIScript>().choices.Length;i++){
								GetComponent<GUIScript>().displayedChoices[i]=GetComponent<GUIScript>().choices[i];
							}
						}
					}
					else{
						if(GetComponent<GUIScript>().displayedText.Equals(GetComponent<GUIScript>().text)){
							GetComponent<GUIScript>().CloseDialogue();
						}
						else{
							GetComponent<GUIScript>().StopAllCoroutines();
							GetComponent<GUIScript>().displayedText = GetComponent<GUIScript>().text;
							for(int i=0;i<GetComponent<GUIScript>().choices.Length;i++){
								GetComponent<GUIScript>().displayedChoices[i]=GetComponent<GUIScript>().choices[i];
							}
						}
					}
				}
			}
			else{
				if(GetComponent<GUIScript>().showInventory){
					for(int i=0;i<10;i++){
						if(GetComponent<GUIScript>().itemCameras[i].pixelRect.Contains(Input.mousePosition)){
							if(!GetComponent<GUIScript>().showExamine){
								GetComponent<GUIScript>().inventorySelect.pixelInset = GetComponent<GUIScript>().itemCameras[i].pixelRect;
							}
							GetComponent<GUIScript>().inventorySelect.enabled = true;
							if (Input.GetButton ("Fire1")) {
								if(!GetComponent<GUIScript>().showExamine){
									if(i<GetComponent<InventoryScript>().GetInventory().Count){
										GetComponent<GUIScript>().ShowContextMenu(i);
									}
								}
								disableRay = true;
							}
						}
					}
				}
				/*
				if(!inventoryButton.Contains(Input.mousePosition)){
					if (Input.GetButtonDown ("Fire1")) {
						if(GetComponent<GUIScript>().showPauseMenu){
							if(GetComponent<GUIScript>().guiArea.Contains(Input.mousePosition)){
								disableRay = true;
							}
						}
						if(GetComponent<GUIScript>().showExamine){
							disableRay = true;
						}
						if(GetComponent<GUIScript>().showLookAt){
							disableRay = true;
						}
						
						//disable moving/raycast when on top of gui components
						if(GetComponent<GUIScript>().showItemMenu){
							foreach(Rect area in GetComponent<GUIScript>().GetGuiAreas()){
								if(area.Contains(Input.mousePosition)){
									disableRay = true;
								}
							}
						}
						if(!disableRay){
							Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
							RaycastHit hit;
							Physics.Raycast(ray, out hit, 10000);
							switch(hit.transform.tag){
							case "item":
								if(Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position,hit.transform.position)<pickupRadius){
									GetComponent<GUIScript>().ShowContextMenu(hit);
									//Debug.Log (hit.transform.name);
								}
								break;
							case "map":
								if(Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position,hit.transform.position)<pickupRadius+1){
									GetComponent<GUIScript>().ShowContextMenu(hit);
								}
								break;
							case "NPC":
								switch(hit.collider.name){
								case "TestNPC":
									hit.collider.GetComponent<TestNPCScript>().Talk();
									GetComponent<GUIScript>().talkingNPC = hit.collider.gameObject;
									break;
								default:
									break;
								}
								break;
							default :
								//Debug.Log("Moving");
								player.GetComponent<Movement>().Move(hit);
								break;
							}
						}
						disableRay = false;
					}
				}
				*/

				/*
				if(Input.GetKeyDown("escape")){
					if(GetComponent<GUIScript>().showPauseMenu){
						GetComponent<GUIScript>().showPauseMenu= false;
					}
					else{
						GetComponent<GUIScript>().showPauseMenu = true;
					}
				}
				*/

				/*
				//user interacts while examining object
				if(GetComponent<GUIScript>().showExamine){
					if(Input.GetMouseButtonDown(0)){
						oldPosition = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
						dragging = true;
					}
					if(Input.GetMouseButtonUp(0)){
						dragging = false;
					}
					if(dragging){
						Vector2 dragDelta = new Vector2(Input.mousePosition.x - oldPosition.x,Input.mousePosition.y - oldPosition.y);
						int index = GetComponent<GUIScript>().itemNumber;
						GetComponent<InventoryScript>().GetInventory()[index].transform.RotateAround(GetComponent<InventoryScript>().GetInventory()[index].transform.position,-Vector3.up,Time.deltaTime*dragDelta.x*rotationSpeed);
						GetComponent<InventoryScript>().GetInventory()[index].transform.RotateAround(GetComponent<InventoryScript>().GetInventory()[index].transform.position,Vector3.right,Time.deltaTime*dragDelta.y*rotationSpeed);
						oldPosition = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
					}
					if(Input.GetMouseButtonDown(1)){
						GetComponent<GUIScript>().examineCamera.enabled =false;
						int index = GetComponent<GUIScript>().itemNumber;
						GetComponent<InventoryScript>().GetInventory()[index].GetComponent<ItemScript>().rotating = true;
						GetComponent<GUIScript>().showExamine =false;
						GetComponent<GUIScript>().showHelp =false;
						GetComponent<GUIScript>().inventorySelect.enabled = false;
					}
				}
				*/
			//}
		//}
	}
}