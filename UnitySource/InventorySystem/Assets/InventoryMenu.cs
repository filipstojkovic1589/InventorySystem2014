using UnityEngine;
using System.Collections.Generic;

public class InventoryMenu : MonoBehaviour {

	private GameObject player;
	private bool inventory;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		inventory = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			inventory = !inventory;
		}
	}

	void OnGUI(){
		if (inventory){
			showInventory();
			showEqupied();
			showCoins();
		}
	}

	void showInventory(){
		GUI.Box (new Rect(60, 60, 500, 280), "Inventory");

		List<GameObject> pickedObjects = player.GetComponent<Player>().pickedObjects;

		int btnCount = 0;
		int collCount = 0;

		for (int i=0; i<pickedObjects.Count; i++) {
			string objName = pickedObjects[i].transform.name;

			if(btnCount >= 4){
				btnCount = 0;
				collCount++;
			}

			int xPos = collCount * 60;
			int yPos = btnCount * 60;

			if(GUI.Button(new Rect(70 + xPos, 90 + yPos, 50, 50), objName)){
				activateObject(pickedObjects[i], i);
			}
			btnCount++;
		}
	}

	void showEqupied(){
		GUI.Box (new Rect(60, 350, 500, 80), "Equiped");
		int x = 70;

		List<Transform> equipableSlots = player.GetComponent<Player>().equipableSlots;
		for (int i=0; i<equipableSlots.Count; i++) {
			string name = equipableSlots [i].name;
			name = name.Replace ("Slot", "");

			if(equipableSlots[i].childCount <= 0){
				GUI.Button(new Rect(x, 370, 70, 50), name);
			}else{
				string specificName;
				specificName = equipableSlots[i].GetChild(0).transform.name;
				specificName = specificName.Replace("(Clone)", "");
				GUI.Button (new Rect (x, 370, 70, 50), specificName);
			}
			x += 80;
		}
	}

	void showCoins(){
		GUI.Box (new Rect(60, 450, 500, 80), "Coins");
		int coins = player.GetComponent<Player> ().coins;
		GUI.Label (new Rect (80, 480, 100, 20), "Coins: " + coins);
	}

	void activateObject(GameObject obj, int index){
		List<Transform> equipableSlots = player.GetComponent<Player>().equipableSlots;
		for (int i=0; i<equipableSlots.Count; i++) {
			string name = equipableSlots[i].name;
			name = name.Replace("Slot", "");

			if(obj.tag == name){
				if(equipableSlots[i].childCount > 0){
					Destroy (equipableSlots[i].GetChild(0).gameObject);
				}else{
					player.GetComponent<Player>().spawnEquipable(index, equipableSlots[i]);
				}
			}
		}
	}
}
