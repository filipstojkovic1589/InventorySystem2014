using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public List<GameObject> pickedObjects;
	public List<string> pickable;

	public int coins;

	public List<Transform> equipableSlots;

	// Use this for initialization
	void Start () {
		pickedObjects = new List<GameObject>();
		//pickable = new List<string>(new string[]{"Weapon", "Head", "Coin"});
		pickable = new List<string>(new string[]{"Coin"});

		//Dodaj objekte koji mogu da budu pokupljeni "pickable".
		//To se vrsi na osnovu slotova koje smo dodali u "equipableSlots".
		//Npr. imamo dodat "rifleSlot", napravice se "Rifle" pickable tag.
		for (int i=0; i<equipableSlots.Count; i++) {
			string name = equipableSlots[i].name;
			name = name.Replace("Slot", "");
			pickable.Add(name);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(pickable.Contains(hit.gameObject.tag)){
			addObject(hit.gameObject);
			Destroy(hit.gameObject);
		}

		//Ovo se odnosi samo na koine.
		//Ostali objekti ne moraju ovde da se hendluju.
		//Idu direktno u array sa picked objektima.
		if(hit.gameObject.tag.Contains("Coin")){
			coins++;
		}
	}

	void addObject(GameObject obj){
		string objName = obj.transform.name;
		GameObject tempObj = (GameObject)Resources.Load (objName);
		pickedObjects.Add(tempObj);
	}

	public void spawnEquipable(int index, Transform slot){
		GameObject tempObj;
		tempObj = (GameObject)Instantiate (pickedObjects [index], slot.position, pickedObjects [index].transform.rotation);
		tempObj.collider.enabled = false;
		tempObj.transform.parent = slot.transform;
	}
}
