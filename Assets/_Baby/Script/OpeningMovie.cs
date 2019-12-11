using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningMovie : MonoBehaviour {

	public void OnMovieSkip () {
		GameObject.Find("TotalManager").GetComponent<AudioSource> ().Stop ();
		SceneManager.LoadScene ("Home");
	}

}
