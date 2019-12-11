using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllManager : MonoBehaviour {

	Animator anim;

	Rigidbody RB;

	public GameObject Parent;

	public bool IsDead;

	public Vector3 PivotPos;

	public Vector3 StretchPos;

	public Vector3 LastPos;

	public bool IsTouch;

	public bool IsFall;

	public float RunSpeed;

	public float MaxSpeed;
	/*
	float stretchRate;
	public float StretchRate{
		get{return stretchRate;}

		set{stretchRate = value;}
	}*/

	public float WalkMotionThredhold;
	public float RunMotionThreshold;
	public float FastRunThreshold;
	public float FRT;

	public float MaxAnimationSpeed;



	AnimationController A_Cont;

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

	public Action act;

	void Awake () {

		//Parent = transform.parent.gameObject;

		anim = GetComponent<Animator> ();

		RB = Parent.GetComponent<Rigidbody> ();

		A_Cont = GetComponent<AnimationController> ();

		GameObject.Find ("Canvas").GetComponent<GameManager> ().Hero = this.gameObject;
	}
	

	void FixedUpdate () {
		
		if (IsDead)
			return;

		if (Input.GetMouseButton (0)) {
			
			if (IsTouch == false) {

				IsTouch = true;

				PivotPos = Input.mousePosition;

				GameManager.Pad_Base.SetActive (true);
				GameManager.Pad_Stick.SetActive (true);

				GameManager.Pad_Base.transform.position = Input.mousePosition; 

			}

			StretchPos = Input.mousePosition;

			if ((PivotPos - StretchPos).magnitude > 0) {

				Vector3 pos1 = new Vector3 (PivotPos.x, 0, PivotPos.y);

				Vector3 pos2 = new Vector3 (StretchPos.x, 0, StretchPos.y);

				if ((new Vector3(transform.position.x,0f,transform.position.z) - LastPos).magnitude < MaxSpeed) {

					//RB.velocity = Vector3.zero;
					RB.AddForce ((pos2 - pos1) * RunSpeed);
					//RB.AddForce ((pos2 - pos1) * RunSpeed,ForceMode.VelocityChange);


				}


				transform.rotation = Quaternion.LookRotation (pos2 - pos1);


				if (IsFall) {
					act = Action.Fall;
					anim.speed = 1;
				} else {
					if ((PivotPos - StretchPos).magnitude > WalkMotionThredhold) {
					
						if ((PivotPos - StretchPos).magnitude > RunMotionThreshold) {

							if (FRT > FastRunThreshold) {

								act = Action.FastRun;

							} else {

								act = Action.Run;
								StartCoroutine ("SwitchFastRun");
							}
						} else {

							act = Action.Walk;
							StopCoroutine ("SwitchFastRun");
							FRT = 0;
						}

					} else {
					
						act = Action.Crawl;
					}
				}

				//A_Cont.actNum = (int)act;
				A_Cont.AnimeTransition ((int)act);

				anim.speed = (PivotPos - StretchPos).magnitude / RunMotionThreshold;
				if (anim.speed > MaxAnimationSpeed)
					anim.speed = MaxAnimationSpeed;

				GameManager.Pad_Stick.transform.position = StretchPos;


			} else {
				StopCoroutine ("SwitchFastRun");
				FRT = 0;

				GameManager.Pad_Stick.transform.position = PivotPos;
			}

			LastPos = new Vector3(transform.position.x,0f,transform.position.z);
		} 

		if (Input.GetMouseButtonUp (0)) {
			if (IsFall) {
				act = Action.Fall;
			} else {
				act = Action.Idle;
			}
			anim.speed = 1;
			A_Cont.AnimeTransition ((int)act);
			StopCoroutine ("SwitchFastRun");
			FRT = 0;
			IsTouch = false;

			GameManager.Pad_Base.SetActive (false);
			GameManager.Pad_Stick.SetActive (false);

			PivotPos = new Vector3 (0, 0, 0);
			StretchPos = new Vector3 (0, 0, 0);
			LastPos = new Vector3 (0, 0, 0);

		}
		A_Cont.AnimeTransition ((int)act);
	}

	IEnumerator SwitchFastRun(){

		while (true) {

			FRT += 1f;

			yield return new WaitForSeconds (0.01f);
		}
	}

	/*
	private void OnAnimatorIK(int layerIndex)
	{
		anim.SetLookAtWeight(1.0f, 0.8f, 1.0f, 0.0f, 0f);
		anim.SetLookAtPosition(this.targetPos);
	}*/
}
