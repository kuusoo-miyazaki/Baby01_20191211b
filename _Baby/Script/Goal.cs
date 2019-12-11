using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	void Start(){
		GameObject.Find ("Canvas").GetComponent<GameManager> ().GoalObject = this.gameObject;
	}

	void OnTriggerEnter(Collider col){

		if(col.CompareTag("Hero") && col.GetComponent<ControllManager> () != null){

			if (col.GetComponent<ControllManager> ().IsDead == false) {

				GameObject.Find ("Canvas").GetComponent<GameManager> ().GameClear ();

				this.gameObject.transform.parent.gameObject.SetActive (false);

			}

		}

	}
}
