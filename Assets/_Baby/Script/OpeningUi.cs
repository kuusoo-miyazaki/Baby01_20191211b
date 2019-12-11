using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningUi : MonoBehaviour {

	public void OpeningEnd(){

		Time.timeScale = 1;

		GetComponent<Animator> ().updateMode = AnimatorUpdateMode.Normal;
	}
}
