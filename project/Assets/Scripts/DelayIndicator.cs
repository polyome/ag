using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DelayIndicator : MonoBehaviour {
		
		public int speed;
		public Text myText;
		private int counter;
	    public string type;
		
		void Start () {
			counter = 0;
			myText = GetComponent<Text>();
			myText.text= type + " .";
		}
		
		void Update () {
			counter += (int)(speed * Time.deltaTime);
			if (counter < 100) {
				myText.text = type + " .";
				} else if (counter < 200) {
				myText.text = type + " ..";
				} else if (counter < 300) {
				myText.text = type + " ...";
				} else if (counter < 400) {
				myText.text = type + " ....";
				} else if (counter < 500) {
				myText.text = type + " .....";
				} else 
					{
					counter =0;
					}
	}
	}
