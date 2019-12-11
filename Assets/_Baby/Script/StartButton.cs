using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {

	public void OnStartButton () {
		GameObject.Find ("TotalManager").GetComponent<TotalManager> ().OnTapStart();
	}
}
