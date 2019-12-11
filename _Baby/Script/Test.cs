using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

	public GameObject bb;


	void Update () {
		GetComponent<Text>().text = (bb.transform.position.x + " / " + bb.transform.position.y + " / " + bb.transform.position.z).ToString ();
	}
}
