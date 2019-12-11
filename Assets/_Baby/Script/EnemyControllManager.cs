using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllManager : MonoBehaviour {

	public bool IsFinded;

	public float FastRunThreshold;

	public float FRT;

	float DefaultMoveSpeed;

	AnimationController A_Cont;

	public enum Action
	{
		Idle,
		Crawl,
		Walk,
		Run,
		FastRun,
		Fall,
	}

	public Action act;



	void Start () {
		DefaultMoveSpeed = GetComponent <NavMeshAgent> ().speed;

		A_Cont = GetComponent<AnimationController> ();
	}
	

	void Update () {

		if (IsFinded) {

			if (FRT > FastRunThreshold) {

				act = Action.FastRun;

			} else {

				act = Action.Run;
				StartCoroutine ("SwitchFastRun");
				GetComponent <NavMeshAgent> ().speed = DefaultMoveSpeed;
			}

		} else {
			act = Action.Idle;
			FRT = 0;
			StopCoroutine ("SwitchFastRun");
			GetComponent <NavMeshAgent> ().speed = 0;
		}

		A_Cont.AnimeTransition ((int)act);
	}


	IEnumerator SwitchFastRun(){

		while (true) {

			FRT += 1f;

			yield return new WaitForSeconds (0.01f);
		}
	}
}
