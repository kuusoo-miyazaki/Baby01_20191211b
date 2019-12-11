using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NendUnityPlugin.AD;

public class HomeManager : MonoBehaviour {

	public GameObject StageButton;

	public GameObject StageListContents;

	public int FinalStageNum;

	public Color StarColor;

	public float ShowAdRate_ReStartStage;

	public float ShowAdRate_BackHome;

	public float ShowAdRate_NextStage;

	public GameObject AdBanner;

	public GameObject LoadingImage;


	void Start () {

		for (int count = 1; PlayerPrefs.GetInt ("StageCleared_Stage_" + string.Format("{0:D3}",count), 0) == 1; count++) {
			
			if (count == FinalStageNum)
				break;

			GameObject _stage = Instantiate (StageButton, transform.position, Quaternion.identity) as GameObject;

			_stage.name = ("S_" + string.Format("{0:D3}",count));

			_stage.transform.SetParent (StageListContents.transform);

			_stage.transform.Find("StageNum").GetComponent<Text> ().text = (count + 1).ToString ();

		}

		AdBanner.GetComponent<NendAdBanner> ().Show();

		TotalManager.FinalStageNum = FinalStageNum;

		TotalManager.ShowAdRate_ReStartStage = ShowAdRate_ReStartStage;

		TotalManager.ShowAdRate_NextStage = ShowAdRate_NextStage;

		TotalManager.ShowAdRate_BackHome = ShowAdRate_BackHome;

	}


	public void OnResetStageClearState(){//テスト用

		PlayerPrefs.DeleteAll();

		SceneManager.LoadScene ("Home");

	}

	public void ShowAdBanner(){

		AdBanner.GetComponent<NendAdBanner> ().Show ();
	}

	public void HideAdBanner(){

		AdBanner.GetComponent<NendAdBanner> ().Hide ();
	}

	public void OnReturnTitle(){

		SceneManager.LoadScene ("Title");

	}
}
