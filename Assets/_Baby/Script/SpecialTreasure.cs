using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialTreasure : MonoBehaviour {

	GameObject GM;

	void Awake () {
		GM = GameObject.Find ("Canvas");
	}


	void OnTriggerEnter (Collider col) {

		if (col.CompareTag ("Hero") && col.GetComponent<ControllManager> () != null) {

			GM.GetComponent<GameManager> ().SpTreasureGetText.SetActive (true);
			GM.GetComponent<GameManager> ().Achievement_GetSpecialCube = true;

			this.transform.parent.gameObject.SetActive (false);

		}
	}
}
