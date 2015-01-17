using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public Texture background;
	string[] newGame;
	string path;

	// Use this for initialization
	void Start () {
		newGame = new string[1];
		path = System.Environment.CurrentDirectory;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUILayout.BeginArea (new Rect( Screen.width/2-200,Screen.height/2-100,400,200));
		GUILayout.BeginVertical ();
		if(GUILayout.Button ("New game")){
			newGame[0] = "0";
			System.IO.File.WriteAllLines(System.IO.Path.Combine(path,"loading.txt"),newGame);
			Application.LoadLevel("Test");
		}
		if(GUILayout.Button ("Load")){
			newGame[0] = "1";
			System.IO.File.WriteAllLines(System.IO.Path.Combine(path,"loading.txt"),newGame);
			Application.LoadLevel("Test");
		}
		if(GUILayout.Button ("Exit")){
			Application.Quit();
		}
		GUILayout.EndVertical ();
		GUILayout.EndArea();
	}
}
