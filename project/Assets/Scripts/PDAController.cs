using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PDAController : MonoBehaviour {

	public struct JournalEntry{
		public string title;
		public string entry;
		}

	public List<JournalEntry> journal = new List<JournalEntry>();
	public Text journalTXT;

	// Use this for initialization
	void Start () {
		Debug.Log ("Start()");
		/* For testing only: InitList (); */
	}
	

	/*
	void InitList () {
		Debug.Log ("InitList");
		AddJournalEntry("Looking around","This place seems unfamiliar to me. I should take a look around.");
		AddJournalEntry ("Test 2", "doing something here...");
		AddJournalEntry ("Test 3", "Huhuu");
		AddJournalEntry ("Test 4", "Heippa!");
	}
	*/

	public void AddJournalEntry(string header, string content){
				JournalEntry addition = new JournalEntry ();
				addition.title = header;
				addition.entry = content;
				journal.Add (addition);
				Debug.Log (header + " " + content);
	}

	public void ShowList() {

		Debug.Log ("ShowList");
		journalTXT.text = " ";

		for(int i = 0; i < journal.Count; i++)
			{
			Debug.Log (journal[i]);
			journalTXT.text = journalTXT.text + "\n" + journal[i].title + " " + journal[i].entry;
			}
	}

}
