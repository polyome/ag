using UnityEngine;
using System.Collections;

public class TimeLimit : MonoBehaviour {
	
	int second;
	int minute;
	float time;
	
	// Use this for initialization
	private void SecondReset(){
		time = 60;
	}
	void Start () {
		minute = 5;
		time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (minute == 0 && second == 0) {
			//時間切れになった時
			guiText.text = minute.ToString () + ":0"+second.ToString();
			Application.LoadLevel("main menu");
			return;
		}
		
		
		if ((int)time ==60){
			minute = minute-1;
		}
		time -= Time.deltaTime;
		second = (int)time;
		if ((int)second == 60) {
			guiText.text = minute.ToString () + ":00";
		} 
		else {
			if((int)second<10){
				if(time < 0.0){
					guiText.text = minute.ToString () + ":0"+second.ToString();
					SecondReset();
				}
				else{
					guiText.text = minute.ToString () + ":0"+second.ToString();
				}
			}
			else{
				guiText.text = minute.ToString () + ":"+second.ToString();
			}
		}
		
		
	}
}