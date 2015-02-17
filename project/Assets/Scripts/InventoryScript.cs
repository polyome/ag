using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class InventoryScript : MonoBehaviour {
	public List<GameObject> inventory =new List<GameObject>();
	public GameObject[] rooms;
	public Transform itemCamera;
	public GameObject bandage;
	public GameObject note;
	public GameObject aimeePhoto;
	public bool usePill = false;
	public bool pickCube = false;

	Transform newCamera;
	// Use this for initialization
	void Start () {
		GameObject[] tempRooms = GameObject.FindGameObjectsWithTag ("Room");
		rooms = new GameObject[tempRooms.Length];
		foreach(GameObject room in tempRooms){
			rooms[room.GetComponent<RoomTriggerer>().roomID-1]=room;
		}
		if(GetComponent<SaveGameScript>().isNewGame){
			pickup(bandage);
			pickup (note);
			pickup (aimeePhoto);
		}
	}
	// Update is called once per frame
	void Update () {
	}
	public List<GameObject> GetInventory(){
		return inventory;
	}

	/*
	// pick up item
	public void pickup(RaycastHit hit){
		hit.collider.transform.position = new Vector3(inventory.Count *5,-10,0);
		inventory.Add(hit.collider.gameObject);
		hit.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
		hit.collider.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
		hit.collider.transform.localScale = new Vector3(1,1,1);
		hit.collider.gameObject.layer = 8;
		hit.collider.gameObject.GetComponent<ItemScript>().rotating = true;
	}
	*/

	// pick up item
	public void pickup(GameObject item){
		//item.transform.position = new Vector3(inventory.Count *5,-10,0);
		//Debug.Log ("Addind "+item.ToString()+" to inventory");

		inventory.Add(item);
		item.GetComponent<Rigidbody>().useGravity = false;
		item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
		item.transform.localScale = new Vector3(1,1,1);
		item.layer = 8;
		item.GetComponent<ItemScript>().rotating = true;
		ArrangeInventory ();
	}

	void ArrangeInventory(){
		for(int i=0;i<inventory.Count;i++){
			inventory[i].transform.position = new Vector3(i*5,-50,0);
			//Debug.Log (inventory[i].ToString()+inventory[i].transform.position.ToString());
		}
	}

	public void RemoveItem(GameObject item){
		inventory.Remove (item);
		GetComponent<SaveGameScript> ().destroyedItems.Add (item.name);
		Destroy (item);
		ArrangeInventory ();
	}
}