using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

	void OnTriggerEnter(Collider col){

		if(col.CompareTag("Hero") && col.GetComponent<ControllManager> () != null){

			GameObject.Find ("Canvas").GetComponent<GameManager> ().LastPortal = this.gameObject;

			GetComponent<Animator> ().SetTrigger("Enter");

			transform.Find ("Text_Save").gameObject.SetActive (true);
		}

	}

	void OnTriggerExit(Collider col){

		if (col.CompareTag ("Hero") && col.GetComponent<ControllManager> () != null) {
			GetComponent<Animator> ().SetTrigger ("Exit");
			transform.Find ("Text_Save").gameObject.SetActive (false);
		}

	}
}
