using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTimer : MonoBehaviour {

	public GameObject Effect;
	public GameObject Spark;

	public bool IsFire;

	public bool IsGenerated;

	public float BombTime;
	public float LastTime;

	void OnTriggerStay (Collider col) {

		if (col.CompareTag ("Hero")) {

			Spark.SetActive (true);

			if(IsFire)
			StartCoroutine ("CountDown");

			IsFire = true;
		}

	}


	IEnumerator CountDown(){

		LastTime = BombTime;

		while(true){
			
			LastTime -= 1f;

			if (LastTime <= 0) {

				GameObject effect = Instantiate (Effect, transform.position, Quaternion.identity) as GameObject;
				Destroy (effect, 1f);

				transform.parent.gameObject.SetActive (false);
				Spark.SetActive (false);

				if (IsGenerated == false) {
					Invoke ("Respawn", 3f);
				} else {
					GameObject.Find ("Canvas").GetComponent<BombGenerator> ().Bombs.Remove (transform.parent.transform.parent.gameObject);
					Destroy (transform.parent.transform.parent.gameObject);
				}

				StopCoroutine ("CountDown");
				LastTime = BombTime;
				IsFire = false;

				yield break;
			}

			yield return new WaitForSeconds (0.05f);
		}

	}

	void Respawn(){

		transform.parent.gameObject.SetActive (true);

	}
}
