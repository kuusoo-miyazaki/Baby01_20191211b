using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCameraMove : MonoBehaviour {

	public List<GameObject> pos = new List<GameObject> ();

	public float Speed;

	public float EndTime;

	public float timecount;

	public GameObject Hero;

	public GameObject PosGroup;

	public GameObject LookPos;


	void Start () {

		foreach (Transform _cp in PosGroup.transform) {
			pos.Add(_cp.gameObject);
		}

		foreach (GameObject _p in pos) {
			_p.GetComponent<MeshRenderer> ().enabled = false;
		}

		StartCoroutine ("MoveCam");
	}
	

	IEnumerator MoveCam () {

		timecount = 0;

		int p1 = Random.Range (0, pos.Count - 1);

		while (true) {

			if (timecount >= EndTime) {

				Camera.main.transform.position = pos [Random.Range (0, pos.Count - 1)].transform.position;

				p1 = Random.Range (0, pos.Count - 1);

				timecount = 0;

			}

			timecount++;

			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, pos [p1].transform.position, Speed);

			Camera.main.transform.LookAt (LookPos.transform);

			yield return new WaitForSeconds (0.01f);
		}
	}
}
