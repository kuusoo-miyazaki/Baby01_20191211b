using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour {

	public ControllManager CM;

	void Start(){

		CM = transform.parent.GetComponent<ControllManager> ();
	}

	void OnTriggerStay(Collider col) {
		if (col.CompareTag ("Ground")) {
			CM.IsFall = false;
			CM.act = ControllManager.Action.Idle;
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.CompareTag ("Ground")) {
			CM.IsFall = true;
		}
	}
}
