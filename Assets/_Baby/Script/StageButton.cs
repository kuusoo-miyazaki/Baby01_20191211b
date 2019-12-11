using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour {

	public GameObject[] Star = new GameObject[3];


	void Start(){
		
		if (PlayerPrefs.GetInt (("Achievement_NoDown_Stage_" + string.Format("{0:D3}",int.Parse (transform.Find("StageNum").GetComponent<Text> ().text))), 0) == 1) {
			Star [0].GetComponent<Image> ().color = GameObject.Find("HomeManager").GetComponent<HomeManager>().StarColor;
		}

		if (PlayerPrefs.GetInt (("Achievement_Time_Stage_" + string.Format("{0:D3}",int.Parse (transform.Find("StageNum").GetComponent<Text> ().text))), 0) == 1) {
			Star [1].GetComponent<Image> ().color = GameObject.Find("HomeManager").GetComponent<HomeManager>().StarColor;
		}

		if (PlayerPrefs.GetInt (("Achievement_GetSpecialCube_Stage_" + string.Format("{0:D3}",int.Parse (transform.Find("StageNum").GetComponent<Text> ().text))), 0) == 1) {
			Star [2].GetComponent<Image> ().color = GameObject.Find("HomeManager").GetComponent<HomeManager>().StarColor;
		}

		transform.localScale = new Vector3 (1, 1, 1);

	}


	public void OnStageButton () {

		GameObject.Find ("HomeManager").GetComponent<HomeManager> ().LoadingImage.SetActive (true);

		GameObject.Find ("HomeManager").GetComponent<HomeManager> ().HideAdBanner ();

		TotalManager.NowStageNum = int.Parse(transform.Find("StageNum").GetComponent<Text>().text);

		Invoke ("LoadStart",0.3f);

	}

	public void LoadStart(){
		StartCoroutine (LoadScene("Stage_" + string.Format("{0:D3}",int.Parse(transform.Find("StageNum").GetComponent<Text>().text)), 1.0f ));
	}
		

	private IEnumerator LoadScene( string i_name, float i_waitTime )
	{
		AsyncOperation async    = SceneManager.LoadSceneAsync( i_name);

		//Debug.LogFormat( "[Time:{0}] Load scene={1}", Time.time.ToString( "F2" ), i_name, gameObject );

		// 指定した時間の間は、allowSceneActivation=falseにすることで、読み込みが完了しないようにする。
		if( i_waitTime > 0.0f )
		{
			async.allowSceneActivation  = false;
			yield return new WaitForSeconds( i_waitTime );
			async.allowSceneActivation  = true;
			print ("WWWWWWW");
		}

		//Debug.LogFormat( "[Time:{0}] Waited Loading scene={1}, allowSceneActivation={2}", Time.time.ToString( "F2" ), i_name, async.allowSceneActivation, gameObject );

		yield return async;

		//Debug.LogFormat( "[Time:{0}] Finished Loading scene={1}", Time.time.ToString( "F2" ), i_name, gameObject );
	}
}
