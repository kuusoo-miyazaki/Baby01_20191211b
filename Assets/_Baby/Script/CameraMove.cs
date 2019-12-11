using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public GameObject Target;

	public GameObject RagdollTarget;

	public float Speed;

	Quaternion Rot;

	ControllManager CM;

	GameManager GM;

	public bool NowZoom;

	public float ZoomSpeed;



	void Start(){

		CM = Target.GetComponent<ControllManager> ();

		GM = GameObject.Find("Canvas").GetComponent<GameManager>();

		Rot = transform.rotation;
	}

	void Update () {

		if (CM.IsDead == false) {

				Vector3 pos = Target.transform.position;

				transform.position = new Vector3 (pos.x, pos.y + 4.5f, pos.z - 7.5f);

				transform.rotation = Rot;

		} else {

			if (GM.GameCleared == true && NowZoom == false) {

				StartCoroutine ("ZoomOut");

			} 

			transform.LookAt (RagdollTarget.transform);

		}
	}

	IEnumerator ZoomOut(){

		while (true) {

			transform.position = new Vector3 (transform.position.x, transform.position.y + ZoomSpeed, transform.position.z);

			yield return new WaitForSeconds (0.005f);
		}
	}
}
