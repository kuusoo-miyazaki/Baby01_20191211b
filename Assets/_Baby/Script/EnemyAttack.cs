using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public float impactPower;

	Vector3 impact;

	Rigidbody RB;
	Rigidbody BodyRB;

	public Vector3 LastPos;

	void OnTriggerEnter (Collider col) {

		if (col.CompareTag ("Hero") && col.GetComponent<ControllManager> () != null) {
			col.GetComponent<ForceTest>().ChangeRagdoll();

			Vector3 Dist = (transform.position - LastPos);

			impact = Dist * impactPower * (Dist.magnitude);

			if (impact.x > 10000f)
				impact = new Vector3 (10000f,impact.y,impact.z);

			if (impact.y > 10000f)
				impact = new Vector3 (impact.x,10000f,impact.z);

			if (impact.x > 10000f)
				impact = new Vector3 (impact.x,impact.y,10000f);

			if (impact.x < -10000f)
				impact = new Vector3 (-10000f,impact.y,impact.z);

			if (impact.y < -10000f)
				impact = new Vector3 (impact.x,-10000f,impact.z);

			if (impact.x < -10000f)
				impact = new Vector3 (impact.x,impact.y,-10000f);

			if (col.GetComponent<ControllManager> ().IsDead == false) {
				col.GetComponent<ForceTest> ().Body.GetComponent<Rigidbody> ().AddForce (impact);
				GameObject.Find ("Canvas").GetComponent<GameManager> ().ActivateRestartUi ();

				GameObject _effect = Instantiate (GameObject.Find ("Canvas").GetComponent<GameManager> ().HitEffect, transform.position, Quaternion.identity) as GameObject;

				Destroy (_effect, 1f);
			}

			col.GetComponent<ControllManager> ().IsDead = true;

			GameObject.Find("Canvas").GetComponent<GameManager> ().Achievement_NoDown = false;
			GameObject.Find ("Canvas").GetComponent<GameManager> ().StopCoroutine ("CountTimer");
			GameManager.Pad_Base.SetActive (false);
			GameManager.Pad_Stick.SetActive (false);

		}
	}
}
