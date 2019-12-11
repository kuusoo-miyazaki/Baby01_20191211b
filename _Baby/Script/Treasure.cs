using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {

	GameObject GM;

	void Awake () {
		GM = GameObject.Find ("Canvas");
		GM.GetComponent<GameManager> ().Treasures.Add (this.gameObject);
	}
	

	void OnTriggerEnter (Collider col) {

		if (col.CompareTag ("Hero") && col.GetComponent<ControllManager> () != null) {

			GM.GetComponent<GameManager> ().GetTreasure++;

			this.transform.parent.gameObject.SetActive (false);

		}
	}
}
