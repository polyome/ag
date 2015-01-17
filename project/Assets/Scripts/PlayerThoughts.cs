using UnityEngine;
using System.Collections;

public class PlayerThoughts : MonoBehaviour {

	public string conversationState;
	GameObject manager;
	string message;
	StoryController.JournalEntry journalEntry;
	float startTime;
	public bool inPod=false;

	public AudioClip[] sounds;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag ("GameController");
		if(conversationState==""){
			conversationState = "introduction";
		}
		startTime = Time.time;
		if(conversationState == "introduction"){
			inPod=true;
			GetComponent<NavMeshAgent>().enabled=false;
			GetComponentInChildren<CapsuleCollider>().enabled=false;
			GetComponentInChildren<Rigidbody>().useGravity = false;
			GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(conversationState.Equals("introduction")){
			if(Time.time>startTime+3.0f){
				Talk();
			}
		}
		if(conversationState.Equals("introduction2")){
			if(Time.time>startTime+11.0f){
				Talk();
			}
		}
		if(conversationState.Equals("introduction3")){
			if(Time.time>startTime+19.0f){
				Talk();
			}
		}
		if(conversationState.Equals("introduction4")){
			if(Time.time>startTime+24.0f){
				Talk();
			}
		}
		if(conversationState.Equals("afterPod1")){
			if(Time.time>startTime+33.0f){
				Talk();
			}
		}
	
	}

	public void Talk(){
		switch(conversationState){
		case "introduction":
			audio.PlayOneShot(sounds[0]);
			message ="Okay Ken. Your head is...aching, you can't focus your eyes and your tongue is as dry as the Nevada desert.";
			conversationState = "introduction2";
			break;
		case "introduction2":
			audio.PlayOneShot(sounds[1]);
			message ="Ughh.. just like a good old hangover of mine, but without the funny part of it.";
			conversationState = "introduction3";
			break;
		case "introduction3":
			audio.PlayOneShot(sounds[2]);
			message ="What’s wrong with me?";
			conversationState = "introduction4";
			break;
		case "introduction4":
			audio.PlayOneShot(sounds[3]);
			message ="Is the rest of the crew awake? Hello!";
			//journalEntry.title = "Looking around";
			//journalEntry.entry = "This place seems unfamiliar to me. I should take a look around.";
			//manager.GetComponent<StoryController>().journal.Add (journalEntry);
			manager.GetComponent<StoryController>().AddJournalEntry("Finding medicine","I really need to find some painkillers. I think there should be some in the medical room.");
			//startTime = Time.time;
			conversationState = "afterPod1";

			break;
		case "afterPod1":
			audio.PlayOneShot(sounds[4]);
			message ="Oh man.. Okay, I have to get *coughs* I have to get some pills for myself.";
			conversationState = "afterPod2";
			break;
			/*
		case "":
			break;
		case "":
			break;
		case "":
			break;
			*/
		default:
			break;
		}
		manager.GetComponent<GUIScript> ().PlayerThoughts (message);
	}
}
