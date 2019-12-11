using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TotalManager : MonoBehaviour {

	private static bool created = false;

	public static int NowStageNum;

	public static int FinalStageNum;

	public static float ShowAdRate_ReStartStage;

	public static float ShowAdRate_BackHome;

	public static float ShowAdRate_NextStage;

	public GameObject MoviePlayable;

	public GameObject TitleUi;

	public GameObject OpUi;

	public GameObject MovieButton;

	public GameObject TitleGround;

	public GameObject Hero;



	void Awake () {

		if (!created) {

			DontDestroyOnLoad (this.gameObject);
			created = true;
		} else {

			Destroy (this.gameObject);

		}

		if (PlayerPrefs.GetInt ("AfterFirstFctivation", 0) == 1) {
			MovieButton.SetActive (true);
		}

		Hero.GetComponent<Animator> ().Play("T_" + (Random.Range(1,5)));

	}

	public void OnTapStart(){

		if (PlayerPrefs.GetInt ("AfterFirstFctivation", 0) == 0) {

			PlayerPrefs.SetInt ("AfterFirstFctivation", 1);//PP

			Camera.main.GetComponent<TitleCameraMove> ().enabled = false;
			TitleGround.SetActive (false);
			OpUi.SetActive (true);
			MoviePlayable.SetActive (true);
			TitleUi.SetActive (false);
			GetComponent<AudioSource> ().Play ();

		} else {

			SceneManager.LoadScene ("Home");

		}

	}

	public void OnPlayMovie(){

		Camera.main.GetComponent<TitleCameraMove> ().enabled = false;
		TitleGround.SetActive (false);
		OpUi.SetActive (true);
		MoviePlayable.SetActive (true);
		TitleUi.SetActive (false);
		GetComponent<AudioSource> ().Play ();

	}

}
