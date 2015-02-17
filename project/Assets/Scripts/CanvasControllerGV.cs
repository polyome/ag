using UnityEngine;
using System.Collections;

public class CanvasControllerGV : MonoBehaviour
{

		public GameObject InventoryCanvas;
		public GameObject PDACanvas;
		public GameObject GameViewCanvas;
		public GameObject PauseCanvas;
		public bool showItemOptions;
		public GameObject journal;
		GameObject manager;
		GameObject item;
		GameObject itemOptions;
		bool inInventory;
		GameObject pickUpButton;
		GameObject useButton;
		public GameObject useWithButton;
		public GameObject loadingScreen;
		public int itemNumber;
		public Texture2D useWithCursor;
		public bool targetingUse;
		GameObject player;
		bool usePill = false;

		// Use this for initialization
		void Start ()
		{
				GameViewCanvas = GameObject.FindGameObjectWithTag ("GameViewCanvas");
				InventoryCanvas = GameObject.FindGameObjectWithTag ("InventoryCanvas");
				PDACanvas = GameObject.FindGameObjectWithTag ("PDACanvas");
				PauseCanvas = GameObject.FindGameObjectWithTag ("PauseCanvas");
				manager = GameObject.FindGameObjectWithTag ("GameController");
				itemOptions = GameObject.FindGameObjectWithTag ("ItemOptions");
				pickUpButton = GameObject.FindGameObjectWithTag ("PickUpButton");
				useButton = GameObject.FindGameObjectWithTag ("UseButton");
				journal = GameObject.FindGameObjectWithTag ("Journal");
				player = GameObject.FindGameObjectWithTag ("Player");

				InventoryCanvas.gameObject.SetActive (false);
				PDACanvas.gameObject.SetActive (false);
				PauseCanvas.gameObject.SetActive (false);
				GameViewCanvas.gameObject.SetActive (true);
				itemOptions.SetActive (false);
				journal.SetActive (false);
		
				showItemOptions = false;
				inInventory = false;
				targetingUse = false;

				Time.timeScale = 1;
		}

		public void SaveGameButton ()
		{
				manager.GetComponent<SaveGameScript> ().SaveGame ();
				manager.GetComponent<GUIScript> ().ShowNotification ("Game Saved to: " + manager.GetComponent<SaveGameScript> ().GetSaveFile ());
		}

		public void LoadGameButton ()
		{
				manager.GetComponent<GUIScript> ().showPlayerThoughts = false;
				loadingScreen.SetActive (true);
				manager.GetComponent<SaveGameScript> ().LoadGame ();
		}

		public void PickUpButton ()
		{
				manager.GetComponent<InventoryScript> ().pickup (item);
		}

		public void UseButton ()
		{
				switch (item.tag) {
				case "door":
						if (item.transform.parent.tag.Equals ("door")) {
								item.transform.parent.transform.parent.GetComponent<DoorScript> ().OpenDoor ();
								HideItemOptions ();
						} else {
								item.transform.parent.GetComponent<DoorScript> ().OpenDoor ();
								HideItemOptions ();
						}

						break;
				case "item":
						switch (item.name) {
						case "bandages":
								manager.GetComponent<GUIScript> ().PlayerThoughts ("I need painkillers, not bandages, but this may help to get me out of here…");                                     
								break;
						case "note":
								player.audio.PlayOneShot (item.GetComponent<ItemScript> ().useSound);
								manager.GetComponent<GUIScript> ().PlayerThoughts ("Wait, hold on a sec. How did this thing even get inside my pocket?");
								break;
						case "small torch":
								manager.GetComponent<GUIScript> ().PlayerThoughts ("It run out of juice. How long I’ve been here?");
								break;
						case "broken ID card":
								manager.GetComponent<GUIScript> ().PlayerThoughts ("I can’t, it’s broken. Splits in two, sharp. ");
								break;
						case "aimeephoto":
								player.audio.PlayOneShot (item.GetComponent<ItemScript> ().useSound);
								manager.GetComponent<GUIScript> ().PlayerThoughts ("If I stare at it enough time I can almost remember something… There is a garden and she’s...there, smiling.");
								break;
						case "knife":
								manager.GetComponent<GUIScript> ().PlayerThoughts ("Maybe I could try to break the glass with it...No, it’s impossible. But even so, the temptation…");
								break;
						case "pillbottle":
								manager.GetComponent<InventoryScript> ().RemoveItem (item);
								manager.GetComponent<GUIScript> ().PlayerThoughts ("This should help.");
								manager.GetComponent<StoryController> ().AddJournalEntry ("Found some medicine", "Picked up some painkillers at the medical facility.");
								manager.GetComponent<StoryController> ().AddJournalEntry ("Contact the crew", "I should try to contact the rest of the crew. There's nobody in the pods so they should be awake.");
								usePill = true;
								break;
						case "Cube":
								if (usePill == true)
										Application.LoadLevel ("credit");
								break;
						default:
								break;
						}
						HideItemOptions ();
						break;
				default:
						break;
				}
		}

		public void UseWithButton ()
		{
				targetingUse = true;
				Cursor.SetCursor (useWithCursor, Vector2.zero, CursorMode.Auto);
				HideItemOptions ();
		}

		public void JournalButton ()
		{
				journal.SetActive (true);
		}

		public void ExamineButton ()
		{
				if (InventoryCanvas.activeInHierarchy) {
						manager.GetComponent<GUIScript> ().Examine (itemNumber);
						HideItemOptions ();
						switch (item.name) {
						case "note":
								player.audio.PlayOneShot (item.GetComponent<ItemScript> ().examineSound);
								manager.GetComponent<GUIScript> ().PlayerThoughts (item.GetComponent<ItemScript> ().examineDescription);
								break;
						case "aimeephoto":
								player.audio.PlayOneShot (item.GetComponent<ItemScript> ().examineSound);
								manager.GetComponent<GUIScript> ().PlayerThoughts (item.GetComponent<ItemScript> ().examineDescription);
								break;
						case "bandages":
				//player.audio.PlayOneShot(item.GetComponent<ItemScript>().examineSound);
								manager.GetComponent<GUIScript> ().PlayerThoughts (item.GetComponent<ItemScript> ().examineDescription);
								break;
						default:
								break;
						}
				} else {
						switch (item.tag) {
						case "item":
								manager.GetComponent<GUIScript> ().PlayerThoughts (item.GetComponent<ItemScript> ().examineDescription);
								HideItemOptions ();
								break;
						case "locker":
								manager.GetComponent<GUIScript> ().PlayerThoughts (item.GetComponent<ItemScript> ().examineDescription);
								HideItemOptions ();
								break;
						case "door":
								if (item.transform.parent.tag.Equals ("door")) {
										string help = item.transform.parent.transform.parent.GetComponent<DoorScript> ().Description ();
										manager.GetComponent<GUIScript> ().PlayerThoughts (help);
								} else {
										string help = item.transform.parent.GetComponent<DoorScript> ().Description ();
										manager.GetComponent<GUIScript> ().PlayerThoughts (help);
								}
								HideItemOptions ();
								break;
						default:
								break;
						}
						//manager.GetComponent<GUIScript> ().LookAt(item);
				}
		}

		public void ShowItemOptions (GameObject target)
		{
				useWithButton.SetActive (false);
				if (target.tag.Equals ("door")) {
						bool animationIsPlaying;
						bool doorOpen;
						if (target.transform.parent.tag.Equals ("door")) {
								animationIsPlaying = target.transform.parent.transform.parent.GetComponent<Animation> ().isPlaying;
								doorOpen = !target.transform.parent.transform.parent.GetComponent<DoorScript> ().closed;
						} else {
								animationIsPlaying = target.transform.parent.GetComponent<Animation> ().isPlaying;
								doorOpen = !target.transform.parent.GetComponent<DoorScript> ().closed;
						}
						if (!animationIsPlaying && !doorOpen) {
								item = target;
								//kayaba changed----------
								Vector3 pos;	
								if (GameObject.FindGameObjectWithTag ("FPCamera").GetComponent<Camera> ().enabled == false)
										pos = Camera.main.WorldToScreenPoint (item.transform.position + new Vector3 (0, 1, 0));
								else
										pos = GameObject.FindGameObjectWithTag ("FPCamera").GetComponent<Camera> ().WorldToScreenPoint (item.transform.position + new Vector3 (0, 0.0f, 0));
								//----------
								itemOptions.transform.position = new Vector3 (pos.x - Screen.width / 2, pos.y - Screen.height / 2, 0);
								showItemOptions = true;
								itemOptions.SetActive (true);
								pickUpButton.SetActive (false);
						} else {
								if (doorOpen) {
										Debug.Log ("door already open");
								} else {
										Debug.Log ("door animation is playing");
								}
						}

				} else {
						item = target;
						Vector3 pos;
						// kayaba changed ----------
						if (item.tag.Equals ("locker")) {
								if (GameObject.FindGameObjectWithTag ("FPCamera").GetComponent<Camera> ().enabled == false)
										pos = Camera.main.WorldToScreenPoint (item.transform.position + new Vector3 (0, 0.7f, 0));
								else
										pos = GameObject.FindGameObjectWithTag ("FPCamera").GetComponent<Camera> ().WorldToScreenPoint (item.transform.position + new Vector3 (0, 0.7f, 0));
						} else {
								if (GameObject.FindGameObjectWithTag ("FPCamera").GetComponent<Camera> ().enabled == false)
										pos = Camera.main.WorldToScreenPoint (item.transform.position);
								else
										pos = GameObject.FindGameObjectWithTag ("FPCamera").GetComponent<Camera> ().WorldToScreenPoint (item.transform.position);
						}
						// ----------
						itemOptions.transform.position = new Vector3 (pos.x - Screen.width / 2, pos.y - Screen.height / 2, 0);
						showItemOptions = true;
						itemOptions.SetActive (true);
						if (!target.GetComponent<ItemScript> ().pickable) {
								pickUpButton.SetActive (false);
						}
						if (!target.GetComponent<ItemScript> ().usable) {
								useButton.SetActive (false);
						}
						inInventory = false;
				}
		}

		public void ShowItemOptions (int i)
		{
				itemNumber = i;
				item = manager.GetComponent<InventoryScript> ().inventory [itemNumber];
				Vector2 position = manager.GetComponent<GUIScript> ().itemCameras [i].pixelRect.center;
				itemOptions.transform.position = new Vector3 (position.x - Screen.width / 2, position.y - Screen.height / 2, 0);
				showItemOptions = true;
				pickUpButton.SetActive (false);
				useWithButton.SetActive (true);
				itemOptions.SetActive (true);
				inInventory = true;
		}

		public void HideItemOptions ()
		{
				showItemOptions = false;
				itemOptions.SetActive (false);
				pickUpButton.SetActive (true);
				useButton.SetActive (true);
				manager.GetComponent<GUIScript> ().HideToolTip ();
		}
	
		// Update is called once per frame
		void Update ()
		{

				if (Input.GetMouseButtonDown (1)) {
						ChangeCanvas (0);	
				}

				if (showItemOptions && !inInventory) {
						//kayaba changed---------
						Vector3 pos;
						if (item.tag.Equals ("door")) {
								if (GameObject.FindGameObjectWithTag ("FPCamera").GetComponent<Camera> ().enabled == false)	//kayabachenged //this is original sentence
										pos = Camera.main.WorldToScreenPoint (item.transform.position + new Vector3 (0, 1, 0));
								else
										pos = GameObject.FindGameObjectWithTag ("FPCamera").GetComponent<Camera> ().WorldToScreenPoint (item.transform.position + new Vector3 (0, 1, 0));
								itemOptions.transform.position = new Vector3 (pos.x - Screen.width / 2, pos.y - Screen.height / 2, 0);
						} else {
								if (item.tag.Equals ("locker")) {
										if (GameObject.FindGameObjectWithTag ("FPCamera").GetComponent<Camera> ().enabled == false)
												pos = Camera.main.WorldToScreenPoint (item.transform.position + new Vector3 (0, 0.7f, 0));
										else
												pos = GameObject.FindGameObjectWithTag ("FPCamera").GetComponent<Camera> ().WorldToScreenPoint (item.transform.position + new Vector3 (0, 0.7f, 0));
										itemOptions.transform.position = new Vector3 (pos.x - Screen.width / 2, pos.y - Screen.height / 2, 0);
								} else {
										if (GameObject.FindGameObjectWithTag ("FPCamera").GetComponent<Camera> ().enabled == false)
												pos = Camera.main.WorldToScreenPoint (item.transform.position);
										else
												pos = GameObject.FindGameObjectWithTag ("FPCamera").GetComponent<Camera> ().WorldToScreenPoint (item.transform.position);
										itemOptions.transform.position = new Vector3 (pos.x - Screen.width / 2, pos.y - Screen.height / 2, 0);
								}
						}
						//------------------------
				}
		}

		/*
	public void ChangeToScene (int NextScene) {
		Application.LoadLevel(NextScene);	
	}
	*/

		public void ChangeToScene (string nextScene)
		{
				Application.LoadLevel (nextScene);
		}
	
		public void ChangeCanvas (int canvas)
		{
				GameViewCanvas.gameObject.SetActive (false);
				InventoryCanvas.gameObject.SetActive (false);
				PDACanvas.gameObject.SetActive (false);
				PauseCanvas.gameObject.SetActive (false);

				switch (canvas) {
				// back button in inventory canvas
				case 0:
						Time.timeScale = 1;
						GameViewCanvas.gameObject.SetActive (true);
						manager.GetComponent<GUIScript> ().HideInventory ();
			//manager.GetComponent<GUIScript>().showJournal = false;
						journal.SetActive (false);
						manager.GetComponent<InputController> ().CloseExamine ();
						HideItemOptions ();
						break;

				// inventory button
				case 1:
						InventoryCanvas.gameObject.SetActive (true);
						manager.GetComponent<GUIScript> ().DisplayInventory ();
						HideItemOptions ();
						break;

				// PDA button
				case 2:
						Time.timeScale = 0;
						manager.GetComponent<GUIScript> ().showPlayerThoughts = false;
						PDACanvas.gameObject.SetActive (true);
			//manager.GetComponent<GUIScript>().showJournal = true;
						break;
			
				case 3:
				default:
						Time.timeScale = 0;
						PauseCanvas.gameObject.SetActive (true);
						break;

				}
		}
}
