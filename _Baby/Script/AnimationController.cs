using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationController : MonoBehaviour {

	Animator anim;

	int LastActNum;

	public enum Action
	{
		Idle,
		Crawl,
		Walk,
		Run,
		FastRun,
		Fall,
		Clear,
	}

	//public int actNum;

	public Action act;

	void Start () {

		anim = GetComponent<Animator> ();

	}
	

	public void AnimeTransition(int actNum){

		if (actNum != LastActNum) {

			act = (Action)Enum.ToObject (typeof(Action), actNum);

			anim.SetTrigger (act.ToString ());

			LastActNum = actNum;
		}
	}

}
