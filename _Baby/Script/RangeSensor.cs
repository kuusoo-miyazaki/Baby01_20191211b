using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangeSensor : MonoBehaviour {

	public GameObject Body;

	EnemyMove EM;

	void Start(){

		EM = Body.GetComponent<EnemyMove> ();

		if (EM.IsPatrolMan == false) {
			this.gameObject.SetActive (false);
		}
	}

	void OnTriggerExit (Collider col) {

		if (col.gameObject == Body) {
			EM.IsFindedHero = false;
			EM.NowPatrolPoint = 0;
		}
		
	}
}
