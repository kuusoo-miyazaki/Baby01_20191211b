using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour {

	public Vector3 LastPos;

	void Update () {
		LastPos = transform.position;
	}
}
