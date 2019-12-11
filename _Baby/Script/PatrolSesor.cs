using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSesor : MonoBehaviour {

	EnemyMove EM;

	ForceTest FT;

	void Start(){

		EM = transform.parent.GetComponent<EnemyMove> ();

		if (EM.IsPatrolMan == false) {
			this.gameObject.SetActive (false);
		}
	}

	void OnTriggerEnter (Collider col) {

		if(col.CompareTag("Hero") && col.GetComponent<ForceTest>() != null){
			
			if(col.GetComponent<ForceTest>().IsEnemy == false && col.GetComponent<ControllManager>().IsDead == false){
				
				EM.IsFindedHero = true;

			}
		}
	}
}
