using UnityEngine;
using System.Collections;



// オブジェクトを点滅させるクラス
public class Blinker : MonoBehaviour {
	private float var;

	
	// 点滅コルーチンを開始する
	void Start() {
		StartCoroutine("Blink");
	}
	
	// 点滅コルーチン
	IEnumerator Blink() {
		while ( true ) {
			this.gameObject.light.enabled = !this.gameObject.light.enabled;
			var = Random.Range(0.1f, 0.75f);
			yield return new WaitForSeconds(var);
		}
	}
}
/*public class Blinker : MonoBehaviour {
	private float nextTime;
	private float var;


	// 初期化
	void Start() {
		nextTime = Time.time;
	}
	// Update is called once per frame
	void Update() {
		if ( Time.time > nextTime ) {
			this.gameObject.light.enabled = !this.gameObject.light.enabled;



			/*Debug.Log ("test0");
			if(this.gameObject.light.intensity == 0f){
				Debug.Log("test1");
				this.gameObject.light.intensity = 1.06f;
			}
			else{
				Debug.Log("test3");
				this.gameObject.light.intensity = 0f;
			}


			var = Random.Range(0.0f, 9.0f);
			nextTime +=  var;
		*/