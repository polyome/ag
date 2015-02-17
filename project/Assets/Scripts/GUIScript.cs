using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GUIScript : MonoBehaviour
{

		public bool showInventory;
		public bool showItemMenu;
		public bool showPauseMenu;
		public bool showExamine;
		public bool showHelp;
		public bool showDialogue;
		public bool showNotification;
		public bool showLookAt;
		public bool showItemTooltip;
		public bool showJournal;
		public bool showPlayerThoughts;
		public bool showToolTip;
		public RaycastHit item;
		public Camera itemCamera;
		public Camera examineCamera;
		public Camera[] itemCameras;
		public Texture[] buttonImages;
		public GUITexture itemSelection;
		public GUITexture inventorySelect;
		public GUITexture inventoryBack;
		public GUITexture dialogueBackground;
		public GUITexture[] inventoryBackground;
		public Rect guiArea;
		public int itemNumber;
		public string text;
		public string[] choices;
		public string displayedText;
		public string[] displayedChoices;
		public string toolTip;
		public GameObject talkingNPC;
		public Vector2 scrollPosition;
		public Font spaceFont;
		GameObject player;
		GUITexture newItemBack;
		GUITexture dialogueBack;
		bool inInventory;
		Rect combineButton;
		Rect examineButton;
		Rect useButton;
		Rect pickupButton;
		Rect dialogueBox;
		float typeSpeed = 0.04f;
		float notificationStartTime;
		float displayTime = 8.0f;
		string notificationText;
		string help;
		GameObject canvasControl;
		GameObject journal;

		// Use this for initialization
		void Start ()
		{
				showJournal = false;
				showItemTooltip = false;
				showLookAt = false;
				showNotification = false;
				showDialogue = false;
				showHelp = false;
				showExamine = false;
				showInventory = false;
				showItemMenu = false;
				showPauseMenu = false;
				examineCamera.enabled = false;
				//showPlayerThoughts = false;
				dialogueBox = new Rect (Screen.width / 6, Screen.height / 4 * 3, Screen.width / 6 * 4, Screen.height / 4);
				dialogueBack = Instantiate (dialogueBackground, Vector3.zero, new Quaternion (0, 0, 0, 0))as GUITexture;
				dialogueBack.pixelInset = new Rect (dialogueBox.x, Screen.height - dialogueBox.y - dialogueBox.height, dialogueBox.width, dialogueBox.height);
				dialogueBack.enabled = false;
				itemCameras = new Camera[10];
				inventoryBackground = new GUITexture[10];
				inventorySelect = Instantiate (itemSelection, Vector3.zero, new Quaternion (0, 0, 0, 0))as GUITexture;
				inventorySelect.enabled = false;
				guiArea = new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 200);
				for (int i=0; i<10; i++) {
						Camera newCamera;
						newCamera = Instantiate (itemCamera, new Vector3 (i * 5, -50, -2), new Quaternion (0, 0, 0, 0))as Camera;
						newCamera.tag = "itemcamera";
						Rect cameraPosition = new Rect (Screen.width / 2 - 350 + i * 70, 50, 70, 70);
						newCamera.camera.pixelRect = cameraPosition;
						newItemBack = Instantiate (inventoryBack, Vector3.zero, new Quaternion (0, 0, 0, 0))as GUITexture;
						newItemBack.pixelInset = cameraPosition;
						inventoryBackground [i] = newItemBack;
						inventoryBackground [i].enabled = false;
						itemCameras [i] = newCamera;
						itemCameras [i].enabled = false;
				}
				scrollPosition = Vector2.zero;
				player = GameObject.FindGameObjectWithTag ("Player");
				canvasControl = GameObject.FindGameObjectWithTag ("canvasControl");
				journal = GameObject.FindGameObjectWithTag ("Journal");


		}
		// Update is called once per frame
		void Update ()
		{
		}

		public void ShowToolTip (string tooltip)
		{
				toolTip = tooltip;
				showToolTip = true;
		}

		public void HideToolTip ()
		{
				showToolTip = false;
		}

		//return context menu button rectangles
		public Rect[] GetGuiAreas ()
		{
				if (inInventory) {
						Rect[] areas = {new Rect (combineButton.x, Screen.height - combineButton.y - 50, combineButton.width, combineButton.height),
				new Rect (examineButton.x, Screen.height - examineButton.y - 50, examineButton.width, examineButton.height),
				new Rect (useButton.x, Screen.height - useButton.y - 50, useButton.width, useButton.height)};
						return areas;
				} else {
						Rect[] areas = {new Rect (pickupButton.x, Screen.height - pickupButton.y - 50, pickupButton.width, pickupButton.height),
				new Rect (examineButton.x, Screen.height - examineButton.y - 50, examineButton.width, examineButton.height),
				new Rect (useButton.x, Screen.height - useButton.y - 50, useButton.width, useButton.height)};
						return areas;
				}
		}

		public void LookAt (GameObject target)
		{
				examineCamera.enabled = true;
				examineCamera.cullingMask = Camera.main.cullingMask;
				examineCamera.orthographic = false;
				examineCamera.pixelRect = new Rect (0, 0, Screen.width, Screen.height);
				examineCamera.clearFlags = Camera.main.clearFlags;
				examineCamera.transform.position = GameObject.Find ("Dummy").transform.position + new Vector3 (0, 1.6f, 0);
				examineCamera.transform.LookAt (target.transform.position);
				examineCamera.fieldOfView = 30;
				showLookAt = true;
				//showItemMenu = false;
				canvasControl.GetComponent<CanvasControllerGV> ().HideItemOptions ();
				showHelp = true;
				help = "Move cursor to edge of screen to scroll; right click to close.";
		}

		//show player thoughts as floating text above his head
		public void PlayerThoughts (string message)
		{
				text = message;
				displayedText = "";
				notificationStartTime = Time.time;
				showPlayerThoughts = true;
				displayedText = text;
				//StartCoroutine (TypeText());
		}

		// show context menu for interactive object
		public void ShowContextMenu (RaycastHit hit)
		{
				item = hit;
				inInventory = false;
				showItemMenu = true;
		}
		// show context menu for item in inventory
		public void ShowContextMenu (int i)
		{
				inInventory = true;
				itemNumber = i;
				showItemMenu = true;
		}
		//show inventory
		public void DisplayInventory ()
		{
				for (int i=0; i<10; i++) {
						itemCameras [i].camera.enabled = true;
						inventoryBackground [i].enabled = true;
				}
				showInventory = true;
		}
		//hide inventory
		public void HideInventory ()
		{
				for (int i=0; i<10; i++) {
						itemCameras [i].camera.enabled = false;
						inventoryBackground [i].enabled = false;
				}
				showItemMenu = false;
				inventorySelect.enabled = false;
				showInventory = false;
		}
		//display dialogue
		public void DisplayDialogue (string message, string[] options)
		{
				Debug.Log ("test!");

				HideInventory ();
				dialogueBack.enabled = true;
				displayedText = "";
				text = message;
				showDialogue = true;
				choices = new string[options.Length];
				displayedChoices = new string[options.Length];
				for (int i=0; i<options.Length; i++) {
						displayedChoices [i] = "";
						choices [i] = options [i];
				}
				StartCoroutine (TypeText ());
		}
		//hide dialogue
		public void CloseDialogue ()
		{
				dialogueBack.enabled = false;
				showDialogue = false;
		}

		public void Examine (int item)
		{
				Debug.Log ("examineCamera:" + examineCamera);

				showExamine = true;
				GetComponent<InventoryScript> ().GetInventory () [item].GetComponent<ItemScript> ().rotating = false;
				examineCamera.enabled = true;
				examineCamera.CopyFrom (itemCameras [item]);
				examineCamera.pixelRect = new Rect (0, 0, Screen.width, Screen.height);
				examineCamera.orthographic = false;
				showItemMenu = false;
				showHelp = true;
				help = "Left click and drag to rotate; right click to close";
		}

		//type text for dialogues
		IEnumerator TypeText ()
		{
				foreach (char letter in text.ToCharArray()) {
						displayedText += letter;
						yield return new WaitForSeconds (typeSpeed);
				}
				if (choices.Length > 0) {
						StartCoroutine (TypeText (0));
				}
		}
		//type text for dialogue choices
		IEnumerator TypeText (int i)
		{
				foreach (char letter in choices[i].ToCharArray()) {
						displayedChoices [i] += letter;
						yield return new WaitForSeconds (typeSpeed);
				}
				if (i + 1 < choices.Length) {
						StartCoroutine (TypeText (i + 1));
				}
		}

		public void ShowNotification (string textToDisplay)
		{
				notificationStartTime = Time.time;
				notificationText = textToDisplay;
				showNotification = true;
		}

		void OnGUI ()
		{
				//display dialogue
				if (showDialogue) {
						GUIStyle dialogueStyle = new GUIStyle (GUI.skin.GetStyle ("label"));
						dialogueStyle.alignment = TextAnchor.UpperLeft;
						dialogueStyle.fontSize = 20;
						GUI.Label (new Rect (dialogueBox.x, dialogueBox.y, dialogueBox.width, dialogueBox.height / 4), displayedText, dialogueStyle);
						//display dialogue options
						GUIStyle choiceStyle = new GUIStyle (GUI.skin.GetStyle ("button"));
						choiceStyle.alignment = TextAnchor.UpperLeft;
						choiceStyle.fontSize = 20;
						choiceStyle.hover.textColor = Color.cyan;
						for (int i=0; i<choices.Length; i++) {
								if (GUI.Button (new Rect (dialogueBox.x, dialogueBox.y + (i + 1) * dialogueBox.height / 4, dialogueBox.width, dialogueBox.height / 4), displayedChoices [i], choiceStyle)) {
										CloseDialogue ();
										switch (talkingNPC.name) {
										case "TestNPC":
												talkingNPC.GetComponent<TestNPCScript> ().Talk (i);
												break;
										default:
												break;
										}
								}
						}
				}
				//show player thoughts
				if (showPlayerThoughts) {
						GUIStyle thoughtStyle = new GUIStyle (GUI.skin.GetStyle ("label"));
						thoughtStyle.alignment = TextAnchor.UpperCenter;
						thoughtStyle.fontSize = 18;
						thoughtStyle.normal.textColor = Color.white;
						thoughtStyle.font = spaceFont;

						Vector3 screenPos;
						if (player.GetComponent<PlayerThoughts> ().inPod) {
								//kayaba changed
								if (GameObject.FindGameObjectWithTag ("FPCamera").GetComponent<Camera> ().enabled == false)
										screenPos = Camera.main.WorldToScreenPoint (player.transform.position + new Vector3 (0, 1.3f, 0));
								else
										screenPos = GameObject.FindGameObjectWithTag ("FPCamera").GetComponent<Camera> ().WorldToScreenPoint (player.transform.position + new Vector3 (0, 1.3f, 0));
						} else {
								if (GameObject.FindGameObjectWithTag ("FPCamera").GetComponent<Camera> ().enabled == false)
										screenPos = Camera.main.WorldToScreenPoint (player.transform.position + new Vector3 (0, 2.5f, 0));
								else
										screenPos = GameObject.FindGameObjectWithTag ("FPCamera").GetComponent<Camera> ().ViewportToScreenPoint (new Vector3 (0.5f, 0.7f, 0f));
						}
						Rect thoughtBox = new Rect (screenPos.x - 150, Screen.height - screenPos.y - 20, 300, 100);
						GUI.Label (thoughtBox, displayedText, thoughtStyle);
						if (Time.time > notificationStartTime + displayTime) {
								showPlayerThoughts = false;
						}



						/*
			if(text.Equals(displayedText)){
				notificationStartTime = Time.time;
			}
			*/
				}

				//show inventory
				if (!showLookAt) {

						/*
			//inventory button
			Rect inventoryButton = new Rect (Screen.width-100,Screen.height - 50,100,50);
			if(GUI.Button(inventoryButton,"inventory")){
				if(!showInventory){
					DisplayInventory();
				}
				else{
					HideInventory();
				}
				if(showExamine){
					examineCamera.enabled =false;
					GetComponent<InventoryScript>().GetInventory()[itemNumber].GetComponent<ItemScript>().rotating = true;
					showExamine = false;
					showHelp = false;
				}
			}
			*/

						/*
			//journal button
			Rect journalButton = new Rect (Screen.width-100,Screen.height - 100,100,50);
			if(GUI.Button(journalButton,"journal")){
				if(!showJournal){
					showJournal = true;
					/*
					foreach(StoryController.JournalEntry journalEntry in GetComponent<StoryController>().journal){
						Debug.Log("title: "+journalEntry.title);
						Debug.Log("entry: "+journalEntry.entry);
					}
					*/
						/*
				}
				else{
					showJournal = false;
				}
			}
			*/

						//journal
						if (showJournal) {
								Rect journalBox = new Rect (Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 400);

								GUILayout.BeginArea (journalBox);
								scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width (400), GUILayout.Height (400));
								GUILayout.BeginVertical ();
								GUIStyle journalStyle = new GUIStyle (GUI.skin.GetStyle ("box"));
								journalStyle.alignment = TextAnchor.UpperLeft;
								journalStyle.fontSize = 14;
								journalStyle.wordWrap = true;
								GUIStyle titleStyle = new GUIStyle (GUI.skin.GetStyle ("box"));
								titleStyle.alignment = TextAnchor.UpperCenter;
								titleStyle.fontSize = 14;
								titleStyle.wordWrap = true;
								foreach (StoryController.JournalEntry journalEntry in GetComponent<StoryController>().journal) {
										GUILayout.Box (journalEntry.title, titleStyle);
										GUILayout.Box (journalEntry.entry, journalStyle);
								}
								GUILayout.EndVertical ();
								GUILayout.EndScrollView ();
								GUILayout.EndArea ();

						}

				}

				//help for examine view
				if (showHelp) {
						GUIStyle helpStyle = new GUIStyle (GUI.skin.GetStyle ("label"));
						helpStyle.alignment = TextAnchor.UpperCenter;
						helpStyle.fontSize = 14;
						GUI.Label (new Rect (70, Screen.height / 2 - 20, 200, 40), help, helpStyle);
				}
				//show context menu
				if (showItemMenu) {
						//context menu for items in inventory
						if (inInventory) {
								combineButton = new Rect (Screen.width / 2 - 390 + 70 * itemNumber, Screen.height - 130, 50, 50);
								if (GUI.Button (combineButton, new GUIContent (buttonImages [0], "combine"))) {
										//combine item
								}
								//examine item
								examineButton = new Rect (Screen.width / 2 - 340 + 70 * itemNumber, Screen.height - 130, 50, 50);
								if (GUI.Button (examineButton, new GUIContent (buttonImages [1], "examine"))) {
										showExamine = true;
										GetComponent<InventoryScript> ().GetInventory () [itemNumber].GetComponent<ItemScript> ().rotating = false;
										examineCamera.enabled = true;
										examineCamera.CopyFrom (itemCameras [itemNumber]);
										examineCamera.pixelRect = new Rect (0, 0, Screen.width, Screen.height);
										examineCamera.orthographic = false;
										showItemMenu = false;
										showHelp = true;
										help = "Left click and drag to rotate; right click to close";
								}
								useButton = new Rect (Screen.width / 2 - 290 + 70 * itemNumber, Screen.height - 130, 50, 50);
								if (GUI.Button (useButton, new GUIContent (buttonImages [2], "use"))) {
										//use item
								}
								// tooltip for the buttons
								GUIStyle tpStyle = GUI.skin.GetStyle ("label");
								tpStyle.alignment = TextAnchor.UpperCenter;
								tpStyle.fontSize = 15;
								GUI.Label (new Rect (Input.mousePosition.x - 35, Screen.height - Input.mousePosition.y - 50, 70, 20), GUI.tooltip, tpStyle);
						}
			//context menu for interactive objects not in inventory
			else {
								Vector2 pos = new Vector2 (Camera.main.WorldToScreenPoint (item.collider.transform.position).x, Screen.height - Camera.main.WorldToScreenPoint (item.collider.transform.position).y);
								pickupButton = new Rect (pos.x - 75, pos.y - 75, 50, 50);
								examineButton = new Rect (pos.x - 25, pos.y - 75, 50, 50);
								useButton = new Rect (pos.x + 25, pos.y - 75, 50, 50);

								//pick up item
								if (item.collider.GetComponent<ItemScript> ().pickable) {
										if (GUI.Button (pickupButton, new GUIContent (buttonImages [0], "pickup"))) {
												GetComponent<InventoryScript> ().pickup (item.collider.gameObject);
												showItemMenu = false;
										}
								}


								//look at item
								if (GUI.Button (examineButton, new GUIContent (buttonImages [1], "look at"))) {
										examineCamera.enabled = true;
										examineCamera.cullingMask = Camera.main.cullingMask;
										examineCamera.orthographic = false;
										examineCamera.pixelRect = new Rect (0, 0, Screen.width, Screen.height);
										examineCamera.clearFlags = Camera.main.clearFlags;
										examineCamera.transform.position = GameObject.Find ("Dummy").transform.position + new Vector3 (0, 1.6f, 0);
										examineCamera.transform.LookAt (item.collider.transform.position);
										examineCamera.fieldOfView = 30;
										showLookAt = true;
										showItemMenu = false;
										showHelp = true;
										help = "Move cursor to edge of screen to scroll; right click to close.";
								}

								//use item
								if (item.collider.GetComponent<ItemScript> ().usable) {
										if (GUI.Button (useButton, new GUIContent (buttonImages [2], "use"))) {
												//use item
										}
								}

								// tooltip for the buttons
								GUIStyle tpStyle = new GUIStyle (GUI.skin.GetStyle ("label"));
								tpStyle.alignment = TextAnchor.UpperCenter;
								tpStyle.fontSize = 14;
								GUI.Label (new Rect (Input.mousePosition.x - 35, Screen.height - Input.mousePosition.y - 50, 70, 30), GUI.tooltip, tpStyle);
						}
				}

				/*
		//show save/load menu
		if (showPauseMenu) {
			GUILayout.BeginArea(guiArea);
			GUILayout.BeginVertical();
			if(GUILayout.Button("save game")){
				GetComponent<SaveGameScript>().SaveGame();
				notificationStartTime=Time.time;
				notificationText="Game Saved to: "+GetComponent<SaveGameScript>().GetSaveFile();
				showNotification =true;
			}
			if(GUILayout.Button("load game")){
				GetComponent<SaveGameScript>().LoadGame();
				notificationStartTime=Time.time;
				notificationText="Game Loaded";
				showNotification =true;
			}
			if(GUILayout.Button("main menu")){
				Application.LoadLevel("main menu");
			}
			GUILayout.EndVertical();
			GUILayout.EndArea();
		}
		*/

				//show notification info
				if (showNotification) {
						GUIStyle notificationStyle = new GUIStyle (GUI.skin.GetStyle ("label"));
						notificationStyle.alignment = TextAnchor.UpperCenter;
						notificationStyle.fontSize = 12;
						notificationStyle.normal.textColor = Color.green;
						Rect notificationBox = new Rect (Screen.width / 2 - 250, Screen.height / 5 * 4, 500, 50);
						GUI.Label (notificationBox, notificationText, notificationStyle);
						if (Time.time > notificationStartTime + displayTime) {
								showNotification = false;
						}
				}

				//display tooltip for usable objects
				if (showItemTooltip) {
						string toolTip = "use";
						GUIStyle toolTipStyle = new GUIStyle (GUI.skin.GetStyle ("label"));
						toolTipStyle.alignment = TextAnchor.UpperCenter;
						toolTipStyle.fontSize = 14;
						toolTipStyle.normal.textColor = Color.green;
						Vector2 mousePos = Input.mousePosition;
						Rect toolTipBox = new Rect (mousePos.x - 50, Screen.height - mousePos.y - 30, 100, 30);
						GUI.Label (toolTipBox, toolTip, toolTipStyle);
				}

				if (showToolTip) {
						GUIStyle toolTipStyle = new GUIStyle (GUI.skin.GetStyle ("label"));
						toolTipStyle.alignment = TextAnchor.UpperCenter;
						toolTipStyle.fontSize = 14;
						toolTipStyle.normal.textColor = Color.green;
						Vector2 mousePos = Input.mousePosition;
						Rect toolTipBox = new Rect (mousePos.x - 50, Screen.height - mousePos.y - 30, 100, 30);
						GUI.Label (toolTipBox, toolTip, toolTipStyle);
				}
		}
}