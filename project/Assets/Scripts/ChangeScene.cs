using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {
	public GameObject loadingScreen;
	public GameObject mainMenu;
	public GameObject credits;
	string[] newGame;
	string path;
	static bool nowPlaying =false;

	/*
	private GameObject GameViewCanvas;
	private GameObject InventoryCanvas;
	private GameObject PDACanvas;
	*/
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		newGame = new string[1];
		path = System.Environment.CurrentDirectory;

		if (Application.loadedLevelName == "credit") {
						Debug.Log (nowPlaying);
						Credits ();
				}

		/*
		GameViewCanvas =  GameObject.FindGameObjectWithTag ("GameViewCanvas");
		InventoryCanvas = GameObject.FindGameObjectWithTag ("InventoryCanvas");
		PDACanvas = GameObject.FindGameObjectWithTag ("PDACanvas");

		InventoryCanvas.gameObject.SetActive (false);
		PDACanvas.gameObject.SetActive (false);
		GameViewCanvas.gameObject.SetActive (true);
		*/
	}

	// Update is called once per frame
	public void ChangeToScene (int NextScene) {
		Application.LoadLevel(NextScene);	
	}

	public void NewGame(){
		newGame[0] = "0";
		System.IO.File.WriteAllLines(System.IO.Path.Combine(path,"loading.txt"),newGame);
		mainMenu.SetActive (false);
		loadingScreen.SetActive (true);
		Application.LoadLevel("ship");
	}

	public void LoadGame(){
		newGame[0] = "1";
		System.IO.File.WriteAllLines(System.IO.Path.Combine(path,"loading.txt"),newGame);
		mainMenu.SetActive (false);
		loadingScreen.SetActive (true);
		Application.LoadLevel("ship");
	}

	public void Credits(){
		mainMenu.SetActive (false);
		credits.SetActive (true);
		credits.GetComponent<Credits> ().RollCredits ();
	}

	void Update(){
		if ((Input.GetMouseButton (0) || Input.GetMouseButton (1) || Input.GetMouseButton (2))&& Application.loadedLevelName == "credit")
				Application.LoadLevel ("main menu");
	}

	public void Exit(){
		Application.Quit ();
	}

	/*
	public void ChangeCanvas (int canvas)
	{
		GameViewCanvas.gameObject.SetActive (false);
		InventoryCanvas.gameObject.SetActive (false);
		PDACanvas.gameObject.SetActive (false);

		switch (canvas) 
			{
			case 0:
				GameViewCanvas.gameObject.SetActive (true);
				break;

			case 1:
				InventoryCanvas.gameObject.SetActive (true);
			break;

			case 2:
			default:
				PDACanvas.gameObject.SetActive (true);
			break;

			}
	}
	*/
}
