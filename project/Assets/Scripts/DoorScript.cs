using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	public bool closed= true;
	public bool locked= false;

	Animation anim;
	AudioSource audio;
	public AudioClip audio1;
	public AudioClip audio2;
	public AudioClip audio3;
	public AnimationClip anim1;
	public float waitTime = 7.0f;

	// Use this for initialization
	void Start () { 

		anim = GetComponent<Animation> ();
		audio = GetComponent<AudioSource> ();
		closed = true;
		//OpenDoor();
		//StartCoroutine (Test());
	}
	
	// Update is called once per frame
	void Update () {

	}

	public string Description(){
		if(locked){
			return "It's locked.";
		}else{
			return "It's a door.";
		}
	}

	public void OpenDoor () {
		if(!anim.isPlaying) {	
			if(!locked){
				closed = false;
				StartCoroutine(toggleLink(2f));
				//door open
				//play sound
				audio.PlayOneShot(audio1);
				audio.PlayOneShot(audio2);
				anim.Rewind();
				anim[anim1.name].speed = 3.0f;
				anim.Play(anim1.name);
				StartCoroutine (Test());
			}
			else {
				audio.PlayOneShot(audio3);
			}
		}
	}

	IEnumerator toggleLink(float seconds){
		Debug.Log ("waiting door to open/close");
		yield return new WaitForSeconds (seconds);
		if(transform.Find("doorlink").GetComponent<OffMeshLink>().activated){
			transform.Find("doorlink").GetComponent<OffMeshLink>().activated=false;
			Debug.Log ("link deactivated");
		}else{
			transform.Find("doorlink").GetComponent<OffMeshLink>().activated=true;
			Debug.Log ("link activated");
		}
	}

	public void CloseDoor () {
		if(!anim.isPlaying) {
			closed = true;
			StartCoroutine(toggleLink(4f));
			//close door
			//play sound
			audio.PlayOneShot(audio1);
			anim[anim1.name].speed = -3.0f;
			anim [anim1.name].time = anim [anim1.name].length;
			anim.Play (anim1.name);
		}
	}

	IEnumerator Test () {
		yield return new WaitForSeconds(anim [anim1.name].length+waitTime);
		CloseDoor ();
	}


}
