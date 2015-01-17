using UnityEngine;
using System.Collections;
public class TestNPCScript : MonoBehaviour {

	public string conversationState;
	GameObject manager;
	string message;
	string[] options;
	StoryController.JournalEntry journalEntry;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag ("GameController");
		if(conversationState==""){
			conversationState = "introduction";
		}
	}
	// Update is called once per frame
	void Update () {
	}

	public void Talk(int i){
		conversationState = conversationState+ i.ToString();
		Talk ();
	}
	public void Talk(){
		switch(conversationState){
		case "introduction":
			message ="Hello, is this working?";
			options = new string[3];
			options[0]="1.Yes.";
			options[1]="2.No.";
			options[2]="3.I don't know.";
			break;
		case "introduction0":
			message = "Really?";
			options = new string[2];
			options[0]="1.Yes.";
			options[1]="2.No.";
			break;
		case "introduction00":
			message = "You're too kind.";
			options = new string[0];
			conversationState ="returning";
			break;
		case "introduction01":
			message = "Thought as much.";
			options = new string[0];
			conversationState ="returning";
			break;
		case "introduction1":
			journalEntry.title = "Angered TestNPC";
			journalEntry.entry = "I've met strange man who called himself TestNPC. " +
				"He didn't take criticism very well." +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"THE END";
			if(!manager.GetComponent<StoryController>().journal.Contains(journalEntry)){
				manager.GetComponent<StoryController>().journal.Add(journalEntry);
			}
			message = "What are you doing here then?";
			options = new string[0];
			conversationState ="rejected";
			break;
		case "introduction2":
			message = "Take some time to think about it.";
			options = new string[0];
			conversationState ="introduction";
			break;
		case "returning":
			message = "Want to do me a favor?";
			options=new string[2];
			options[0]="1.Sure, why not.";
			options[1]="2.Sorry, I'm busy.";
			break;
		case "returning0":
			journalEntry.title = "Find the Cube for TestNPC";
			journalEntry.entry = "TestNPC asked me to find the cube and bring it to him" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla";
			if(!manager.GetComponent<StoryController>().journal.Contains(journalEntry)){
				manager.GetComponent<StoryController>().journal.Add(journalEntry);
			}
			if(manager.GetComponent<InventoryScript>().GetInventory().Contains(GameObject.Find("Cube"))){
				message = "That's it! Give it to me, please!";
				options=new string[2];
				options[0]="1.Of course.<hand the cube over>";
				options[1]="2.No, I'm keeping it.";
			}
			else{
				message = "Bring me that cube over there.";
				options=new string[0];
			}
			break;
		case "returning00":
			journalEntry.title = "Gave the Cube to TestNPC";
			journalEntry.entry = "I've given the cube to TestNPC. He is very grateful." +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla" +
					"bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla";
			if(!manager.GetComponent<StoryController>().journal.Contains(journalEntry)){
				manager.GetComponent<StoryController>().journal.Add(journalEntry);
			}
			manager.GetComponent<InventoryScript>().RemoveItem(GameObject.Find ("Cube"));
			message = "Thank you very much!";
			options=new string[0];
			conversationState ="complete";
			break;
		case "complete":
			message = "I won't forget this act of kindness!";
			options=new string[0];
			break;
		case "returning01":
			message = "You'll be back.";
			options=new string[0];
			conversationState = "returning0";
			break;
		case "returning1":
			message = "Come back if you change your mind.";
			options=new string[0];
			conversationState="returning";
			break;
		case "rejected":
			message = "<stare>...";
			options=new string[0];
			break;
		default:
			message = "";
			options=new string[0];
			break;
		}
		manager.GetComponent<GUIScript>().DisplayDialogue(message,options);
	}
}