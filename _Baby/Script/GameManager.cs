using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NendUnityPlugin.AD;
using NendUnityPlugin.AD.Video;

public class GameManager : MonoBehaviour {

	public bool GameCleared;

	public GameObject Hero;

	public GameObject LastPortal;

	public List<GameObject> Enemys = new List<GameObject>();

	public GameObject GoalObject;

	public GameObject OpeningUi;

	public GameObject RestartUi;

	public GameObject TreasureUi;
	public GameObject TreasureUiMax;

	public GameObject GameOverUi;

	public GameObject ClearUi;

	public GameObject ResultUi;

	public GameObject PauseUi;

	public GameObject HeaderUi;

	public GameObject[] Star = new GameObject[3];

	public GameObject TimeLimitText;

	public GameObject TimeUi;

	public static GameObject Pad_Base;

	public static GameObject Pad_Stick;

	public List<GameObject> Treasures = new List<GameObject>();

	public int getTreasure;

	public int GetTreasure{

		get{return getTreasure;}

		set{
			getTreasure = value;

			TreasureUi.GetComponent<Text> ().text = value.ToString();
			TreasureUi.GetComponent<Animator> ().Play ("ScaleChange",0,0.0f);

			if (value == Treasures.Count) {
				GoalObject.SetActive (true);
			}
		}
	}

	public int timeCount;

	public int TimeCount{

		get{return timeCount;}

		set{
			timeCount = value;

			TimeUi.GetComponent<Text> ().text = value.ToString();

			if (value <= 10) {
				TimeUi.GetComponent<Animator> ().Play ("ScaleChange",0,0.0f);
			}
		}
	}

	public float TimeSpeed;

	public int StartTime;

	public int TimeLimit;


	public bool Achievement_NoDown;

	public bool Achievement_Time;

	public bool Achievement_GetSpecialCube;

	public Vector3 HeroStartPos;

	public GameObject SpTreasureGetText;


	string[] DeadText = {"転倒","打撲","絶望","擦傷","昏倒","目眩","昇天","解脱","骨折","離陸","陥没","唖然","火傷","蒸発","消沈"
		,"消沈","捻挫","摩耗","爆散","融解","気化","凍結","爆睡","浮遊","爆笑","褒美","屈服","閉口","堕落","気絶","落胆","研磨"};

	public GameObject HitEffect;

	VideoObject AdMovie;

	VideoObject_5nim AdMovie_5min;

	public GameObject AdBanner;

	public GameObject CloseButton;



	void Start () {

		//SceneManager.UnloadSceneAsync ("Home");

		//OpeningUi.GetComponent<Animator> ().updateMode = AnimatorUpdateMode.UnscaledTime;

		//Time.timeScale = 0;

		OpeningUi.transform.Find ("StageNum").gameObject.GetComponent<Text> ().text = TotalManager.NowStageNum.ToString ();

		OpeningUi.SetActive (true);

		Application.targetFrameRate = 60;

		Pad_Base = GameObject.Find ("Pad_Base");
		Pad_Stick = GameObject.Find ("Pad_Stick");

		Pad_Base.SetActive (false);
		Pad_Stick.SetActive (false);

		TreasureUiMax.GetComponent<Text> ().text = Treasures.Count.ToString();

		TimeCount = StartTime;
		StartCoroutine ("CountTimer");

		Achievement_NoDown = true;
		Achievement_Time = true;
		TimeLimitText.GetComponent<Text> ().text =  ("Within " + TimeLimit.ToString() + " sec clear");

		HeroStartPos = Hero.transform.parent.transform.position;

		GoalObject = GameObject.Find ("Goal");

		GoalObject.SetActive (false);

		AdMovie = GetComponent<VideoObject> ();
		AdMovie_5min = GetComponent<VideoObject_5nim> ();

		AdMovie.Load ();
		AdMovie_5min.Load ();
		
	}
	

	public void RestartPortal () {

		RestartUi.SetActive (false);

		StartCoroutine ("CountTimer");

		if (LastPortal != null) {
			Hero.transform.parent.transform.position = LastPortal.transform.position;
		} else {
			Hero.transform.parent.transform.position = HeroStartPos;
		}

		Hero.GetComponent<ControllManager> ().IsDead = false;

		Hero.GetComponent<ForceTest> ().SetKinematic (true);

		Hero.GetComponent<Animator> ().enabled = true;

		Hero.transform.parent.GetComponent<Rigidbody> ().velocity = Vector3.zero;

		Hero.GetComponent<ControllManager> ().PivotPos = new Vector3 (0, 0, 0);
		Hero.GetComponent<ControllManager> ().StretchPos = new Vector3 (0, 0, 0);
		Hero.GetComponent<ControllManager> ().LastPos = new Vector3 (0, 0, 0);
		Hero.GetComponent<ControllManager> ().IsTouch = false;


		foreach (GameObject enemy in Enemys) {

			enemy.GetComponent<ForceTest> ().SetKinematic (true);

			enemy.GetComponent<Animator> ().enabled = true;

			if (enemy.GetComponent<EnemyMove> ().IsPatrolMan == true) {

				enemy.GetComponent<EnemyMove> ().IsFindedHero = false;

				enemy.GetComponent<EnemyMove> ().NowPatrolPoint = 0;

				enemy.transform.parent.transform.parent.transform.position = enemy.GetComponent<EnemyMove> ().StartPos;

				enemy.transform.parent.transform.localPosition = new Vector3(0,0,0);
				enemy.transform.localPosition = new Vector3(0,0,0);

				enemy.transform.parent.transform.parent.transform.Find("PatrolPoints").transform.localPosition = new Vector3(0,0,0);

				enemy.transform.parent.transform.parent.transform.Find("OuterRange").transform.localPosition = new Vector3(0,0,0);

				enemy.transform.Find("Sensor").transform.localPosition = new Vector3(0,0,0);

				enemy.transform.Find("Sensor").transform.localRotation = new Quaternion(0,0,0,0);

				enemy.GetComponent<NavMeshAgent> ().enabled = true;

				enemy.GetComponent<EnemyMove> ().enabled = true;
			} else {
				enemy.transform.parent.transform.position = enemy.GetComponent<EnemyMove> ().StartPos;

				enemy.transform.localPosition = new Vector3(0,0,0);

				enemy.GetComponent<NavMeshAgent> ().enabled = true;

				enemy.GetComponent<EnemyMove> ().enabled = true;
			}



		}

	}

	public void ActivateRestartUi(){

		RestartUi.transform.Find("Word_Down").gameObject.GetComponent<Text>().text = DeadText[Random.Range(0,DeadText.Length)];

		RestartUi.SetActive (true);

	}


	IEnumerator CountTimer(){

		float count = 0;

		while (true) {
		
			count += TimeSpeed * Time.deltaTime;

			if(count > 1000f){

				TimeCount -= 1;

				if (TimeCount <= 0) {
					GameOver ();
					StopCoroutine ("CountTimer");
					yield return null;
				}

				count = 0;

				if(TimeCount < StartTime - TimeLimit){
					Achievement_Time = false;
				}

			}

			yield return new WaitForSeconds (0.01f);
		}
	}

	public void GameOver(){

		print ("End");

		Hero.GetComponent<ForceTest> ().ChangeRagdoll ();

		Hero.GetComponent<ControllManager> ().IsDead = true;
		Pad_Base.SetActive (false);
		Pad_Stick.SetActive (false);

		foreach (GameObject enemy in Enemys) {

			enemy.GetComponent<ForceTest> ().ChangeRagdoll ();

			enemy.GetComponent<NavMeshAgent> ().enabled = false;

			enemy.GetComponent<EnemyMove> ().enabled = false;

		}

		GameOverUi.SetActive (true);

	}
		

	public void GameClear(){

		GameCleared = true;

		PlayerPrefs.SetInt ( ("StageCleared_" + (SceneManager.GetActiveScene ().name).ToString()), 1);//PP
		PlayerPrefs.Save ();

		ClearUi.SetActive (true);

		AchievementCheck ();

		Invoke("ActiveResultUi", 2f);

		StopCoroutine ("CountTimer");

		Hero.GetComponent<Animator> ().SetTrigger ("Clear");
		//Hero.GetComponent<ControllManager>().act = ControllManager.Action.Clear;

		Hero.GetComponent<ForceTest> ().enabled = false;

		Hero.transform.rotation = new Quaternion (0,180f,0,0);

		Hero.GetComponent<ControllManager> ().IsDead = true;
		Pad_Base.SetActive (false);
		Pad_Stick.SetActive (false);

		foreach (GameObject enemy in Enemys) {

			enemy.GetComponent<ForceTest> ().ChangeRagdoll ();

			enemy.GetComponent<ForceTest> ().enabled = false;

			if(enemy.GetComponent<NavMeshAgent> () != null)
			enemy.GetComponent<NavMeshAgent> ().enabled = false;

			enemy.GetComponent<EnemyMove> ().enabled = false;

		}
	}

	void ActiveResultUi(){
		ClearUi.SetActive (false);

		if (TotalManager.FinalStageNum == TotalManager.NowStageNum) {
			ResultUi.transform.Find ("Button_PlayNextStage").gameObject.SetActive (false);
		}

		ResultUi.transform.Find ("StageNum").GetComponent<Text> ().text = TotalManager.NowStageNum.ToString ();

		HeaderUi.SetActive (false);

		ResultUi.SetActive (true);

		AdBanner.GetComponent<NendAdBanner> ().Show ();
	}

	void AchievementCheck(){

		if (Achievement_NoDown == true) {
			
			Star [0].SetActive (true);

			if (PlayerPrefs.GetInt (("Achievement_NoDown_" + SceneManager.GetActiveScene ().name), 0) == 1) {
				Star [0].GetComponent<Animator> ().enabled = false;
			} else {
				PlayerPrefs.SetInt ( ("Achievement_NoDown_" + SceneManager.GetActiveScene ().name), 1);//PP
				PlayerPrefs.Save ();
			}

		} else {

			if (PlayerPrefs.GetInt (("Achievement_NoDown_" + SceneManager.GetActiveScene ().name), 0) == 1) {
				Star [0].GetComponent<Animator> ().enabled = false;
				Star [0].SetActive (true);
			}

		}

		if (Achievement_Time == true) {

			Star [1].SetActive (true);

			if (PlayerPrefs.GetInt (("Achievement_Time_" + SceneManager.GetActiveScene ().name), 0) == 1) {
				Star [1].GetComponent<Animator> ().enabled = false;
			} else {
				PlayerPrefs.SetInt ( ("Achievement_Time_" + SceneManager.GetActiveScene ().name), 1);//PP
				PlayerPrefs.Save ();
			}

		} else {

			if (PlayerPrefs.GetInt (("Achievement_Time_" + SceneManager.GetActiveScene ().name), 0) == 1) {
				Star [1].GetComponent<Animator> ().enabled = false;
				Star [1].SetActive (true);
			}

		}

		if (Achievement_GetSpecialCube == true) {

			Star [2].SetActive (true);

			if (PlayerPrefs.GetInt (("Achievement_GetSpecialCube_" + SceneManager.GetActiveScene ().name), 0) == 1) {
				Star [2].GetComponent<Animator> ().enabled = false;
			} else {
				PlayerPrefs.SetInt ( ("Achievement_GetSpecialCube_" + SceneManager.GetActiveScene ().name), 1);//PP
				PlayerPrefs.Save ();
			}

		} else {

			if (PlayerPrefs.GetInt (("Achievement_GetSpecialCube_" + SceneManager.GetActiveScene ().name), 0) == 1) {
				Star [2].GetComponent<Animator> ().enabled = false;
				Star [2].SetActive (true);
			}

		}


	}

	public void ReturnHome(){

		if (TotalManager.ShowAdRate_BackHome > Random.Range (0, 100))
			AdMovie_5min.Show ();

		AdBanner.GetComponent<NendAdBanner> ().Hide ();
		
		SceneManager.LoadScene ("Home");
	}

	public void RestartStage(){

		if (TotalManager.ShowAdRate_ReStartStage > Random.Range (0, 100))
			AdMovie_5min.Show ();

		AdBanner.GetComponent<NendAdBanner> ().Hide ();

		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void PlayNextStage(){
		
		if (TotalManager.ShowAdRate_NextStage > Random.Range (0, 100))
			AdMovie.Show ();

		AdBanner.GetComponent<NendAdBanner> ().Hide ();

		TotalManager.NowStageNum += 1;

		SceneManager.LoadScene ("Stage_" + string.Format("{0:D3}",TotalManager.NowStageNum));

	}

	public void PauseIn(){

		if (GameCleared == false && Hero.GetComponent<ControllManager>().IsDead == false) {

			StopCoroutine ("CountTimer");

			Time.timeScale = 0;

			PauseUi.SetActive (true);

		}

	}

	public void PauseOut(){

		StartCoroutine ("CountTimer");

		Time.timeScale = 1;

		PauseUi.SetActive (false);

	}

	public void RestartStageInPause(){

		if (TotalManager.ShowAdRate_ReStartStage > Random.Range (0, 100))
			AdMovie_5min.Show ();

		SceneManager.LoadScene (SceneManager.GetActiveScene().name);

		Time.timeScale = 1;
	}

	public void ReturnHomeInPause(){

		if (TotalManager.ShowAdRate_BackHome > Random.Range (0, 100))
			AdMovie_5min.Show ();

		SceneManager.LoadScene ("Home");

		Time.timeScale = 1;
	}

	public void CloseAdBanner(){
		AdBanner.GetComponent<NendAdBanner> ().Hide();
		CloseButton.SetActive (false);
	}
		
}
