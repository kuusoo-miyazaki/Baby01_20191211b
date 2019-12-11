using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickMove : MonoBehaviour {

	public GameObject MoveObj;

	public GameObject P_a;

	public GameObject P_b;

	public float MoveSpeed;

	public bool MoveTo_a;


	void Start () {

		P_a.GetComponent<MeshRenderer> ().enabled = false;
		P_b.GetComponent<MeshRenderer> ().enabled = false;
		StartCoroutine ("MoveGimmick");
	}
	

	IEnumerator MoveGimmick () {

		//float count = 0;

		while (true) {

			if (MoveTo_a == true) {

				MoveObj.transform.position = Vector3.Lerp (MoveObj.transform.position, P_a.transform.position, MoveSpeed);

				if ((MoveObj.transform.position - P_a.transform.position).magnitude < 0.1f) {
					MoveTo_a = false;
				}

			} else {
				
				MoveObj.transform.position = Vector3.Lerp (MoveObj.transform.position, P_b.transform.position, MoveSpeed);

				if ((MoveObj.transform.position - P_b.transform.position).magnitude < 0.1f)  {
					MoveTo_a = true;
				}
			}

			yield return new WaitForSeconds (0.01f);

		}
	}
}
