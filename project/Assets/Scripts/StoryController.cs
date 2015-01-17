using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StoryController : MonoBehaviour {

	public Text journalTXT;
	public struct JournalEntry{
		public string title;
		public string entry;
	}
	public List<JournalEntry> journal = new List<JournalEntry>();
	public GameObject journalContent;

	// Use this for initialization
	void Start () {
		journalContent = GameObject.FindGameObjectWithTag ("JournalContent");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddJournalEntry(string header, string content){
		JournalEntry addition = new JournalEntry ();
		addition.title = header;
		addition.entry = content;
		journal.Add (addition);
		//Debug.Log (journalUI.ToString());
		UpdateJournal ();
	}

	public void UpdateJournal() {
		
		Debug.Log ("ShowList");
		journalTXT.text = " ";
		
		for(int i = 0; i < journal.Count; i++)
		{
			Debug.Log (journal[i]);
			journalTXT.text = journalTXT.text + "\n"+"\t" +System.DateTime.Now.ToString("MM/dd/yyyy")+"\t"+System.DateTime.Now.ToString("hh:mm:ss")+"\t"+journal[i].title + "\n" + journal[i].entry;
		}
	}
}
