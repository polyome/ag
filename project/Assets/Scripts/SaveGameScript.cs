using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveGameScript : MonoBehaviour {

	public List<string> destroyedItems;
	GameObject manager;
	string[] saveData;
	string[] newGame;
	string path;
	public bool isNewGame = true;

	// Use this for initialization
	void Start () {
		newGame = new string[1];
		path = System.Environment.CurrentDirectory;
		manager = GameObject.FindGameObjectWithTag ("GameController");
		//Debug.Log ("Savegame directory: "+path);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string GetSaveFile(){
		return System.IO.Path.Combine (System.Environment.CurrentDirectory, "saveGame.txt");
	}

	public void SaveGame (){
		List<GameObject> inventory = manager.GetComponent<InventoryScript>().GetInventory();
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		int lenght = inventory.Count;
		saveData = new string[lenght+16+destroyedItems.Count+2*manager.GetComponent<StoryController>().journal.Count];
		saveData[0]="position";
		saveData[1]= player.transform.position.x.ToString();
		saveData[2]= player.transform.position.y.ToString();
		saveData[3]= player.transform.position.z.ToString();
		saveData[4]="rotation";
		saveData[5]= player.transform.rotation.x.ToString();
		saveData[6]= player.transform.rotation.y.ToString();
		saveData[7]= player.transform.rotation.z.ToString();
		saveData[8]= player.transform.rotation.w.ToString();
		saveData[9]="TestNPC";
		saveData [10] = ""; //GameObject.Find("TestNPC").GetComponent<TestNPCScript>().conversationState;
		saveData [11] = "playerThoughts";
		saveData [12] = GameObject.Find("Character").GetComponent<PlayerThoughts> ().conversationState;
		saveData[13]="inventory";
		for(int i=0; i < lenght ; i++){
			saveData[i+14]=inventory[i].name;
		}
		saveData[14+lenght]="destroyedItems";
		for(int j=0;j<destroyedItems.Count;j++){
			saveData[15+lenght+j]=destroyedItems[j];
		}
		saveData[15+lenght+destroyedItems.Count]="journal";
		for(int k=0;k<manager.GetComponent<StoryController>().journal.Count;k++){
			saveData[16+lenght+destroyedItems.Count+2*k]=manager.GetComponent<StoryController>().journal[k].title;
			saveData[17+lenght+destroyedItems.Count+2*k]=manager.GetComponent<StoryController>().journal[k].entry;
		}
		System.IO.File.WriteAllLines(System.IO.Path.Combine(path,"saveGame.txt"),saveData);
	}

	public void LoadGame(){
		newGame[0] = "1";
		System.IO.File.WriteAllLines(System.IO.Path.Combine(path,"loading.txt"),newGame);
		Application.LoadLevel ("ship");
	}

	void OnLevelWasLoaded(int level){
		int loading;
		System.IO.StreamReader newGame = new System.IO.StreamReader (System.IO.Path.Combine(System.Environment.CurrentDirectory,"loading.txt"));
		loading = int.Parse (newGame.ReadLine ());
		newGame.Close ();
		
		if(loading == 1){
			Debug.Log ("loading");
			isNewGame = false;
			System.IO.StreamReader save = new System.IO.StreamReader (System.IO.Path.Combine(System.Environment.CurrentDirectory,"saveGame.txt"));
			string line;
			while ((line = save.ReadLine()) != null) {
				if(line =="position"){
					float x = float.Parse(save.ReadLine());
					float y = float.Parse(save.ReadLine());
					float z = float.Parse(save.ReadLine());
					//GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(x,y,z);
					GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>().Warp(new Vector3(x,y,z));
				}
				if(line == "rotation"){
					float x = float.Parse(save.ReadLine());
					float y = float.Parse(save.ReadLine());
					float z = float.Parse(save.ReadLine());
					float w = float.Parse(save.ReadLine());
					GameObject.FindGameObjectWithTag("Player").transform.rotation = new Quaternion(x,y,z,w);
				}
				if(line == "TestNPC"){
					save.ReadLine();
					//GameObject.Find("TestNPC").GetComponent<TestNPCScript>().conversationState = save.ReadLine();
				}

				if(line == "playerThoughts"){
					GameObject.Find("Character").GetComponent<PlayerThoughts>().conversationState = save.ReadLine();
				}

				if(line=="inventory"){
					while ((line = save.ReadLine()) != "destroyedItems") {
						GetComponent<InventoryScript>().pickup(GameObject.Find(line));
					}
				}
				if(line=="destroyedItems"){
					while((line=save.ReadLine()) != "journal"){
						destroyedItems.Add(line);
					}
				}
				if(line=="journal"){
					while((line=save.ReadLine()) != null){
						StoryController.JournalEntry journalEntry;
						journalEntry.title = line;
						journalEntry.entry = save.ReadLine();
						GameObject.Find("Manager").GetComponent<StoryController>().journal.Add(journalEntry);
					}
				}
			}
			save.Close ();
			foreach(string item in destroyedItems){
				Destroy(GameObject.Find(item));
			}
		}
	}

}
