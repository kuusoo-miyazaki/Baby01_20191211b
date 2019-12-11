using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceTest : MonoBehaviour {

	public float impactPower;

	public GameObject Body;

	public bool IsEnemy;

	Vector3 impact;

	public Vector3 LastPos;

	Animator anim;

	Rigidbody RB;
	Rigidbody BodyRB;

	public GameObject Effect;

	GameObject GM;


	void Start(){
		SetKinematic (true);

		anim = GetComponent<Animator> ();
		RB = transform.parent.GetComponent<Rigidbody> ();
		RB.isKinematic = false;
		BodyRB = Body.GetComponent<Rigidbody> ();

		GM = GameObject.Find ("Canvas");
	}

	void OnTriggerEnter (Collider col) {
		
		if (col.CompareTag("Gimmick")){


			ChangeRagdoll();

			Vector3 Dist = (col.transform.position - col.GetComponent<Pusher> ().LastPos);

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


			BodyRB.AddForce(impact);


			if (GetComponent<ControllManager> () != null) {

				if (GetComponent<ControllManager> ().IsDead == false) {

					GM.GetComponent<GameManager> ().ActivateRestartUi ();

					GameObject _effect = Instantiate (GM.GetComponent<GameManager> ().HitEffect, transform.position, Quaternion.identity) as GameObject;

					Destroy (_effect, 1f);
				}

				GetComponent<ControllManager> ().IsDead = true;
				GM.GetComponent<GameManager> ().Achievement_NoDown = false;
				GM.GetComponent<GameManager> ().StopCoroutine ("CountTimer");
				GameManager.Pad_Base.SetActive (false);
				GameManager.Pad_Stick.SetActive (false);

			}

			if (GetComponent<NavMeshAgent> () != null) {
				GetComponent<NavMeshAgent> ().enabled = false;
				GetComponent<EnemyMove> ().enabled = false;
			}


			//GameObject effect = Instantiate (Effect, transform.position, Quaternion.identity) as GameObject;
			//Destroy (effect, 1f);
		}

	}


	public void ChangeRagdoll(){

		SetKinematic(false);
		anim.enabled = false;
	}

	public void SetKinematic(bool newValue)
	{
		GetComponent<CapsuleCollider> ().enabled = newValue;

		Component[] components=GetComponentsInChildren(typeof(Rigidbody));

		foreach (Component c in components)
		{
			(c as Rigidbody).isKinematic=newValue;
		}

	}

	void Update () {
		LastPos = transform.position;
	}
}
