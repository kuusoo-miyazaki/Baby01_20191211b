using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour {

	public bool IsPatrolMan;

	public bool IsChaser;

	public bool IsFindedHero;

	public bool IsBoss;

	public GameObject MoveTarget;

	public Vector3 StartPos;

	public float DefaultSpeed;

	public float ChaseSpeed;

	NavMeshAgent agent;

	public List<GameObject> PatrolPoint = new List<GameObject> ();

	public int NowPatrolPoint;

	GameObject GM;
	/*
	NavMeshPath path;
	LineRenderer line;
	public GameObject ll;*/

	GameObject LastTarget;


	void Start () {

		GM = GameObject.Find ("Canvas").gameObject;

		MoveTarget = GM.GetComponent<GameManager> ().Hero;

		GM.GetComponent<GameManager> ().Enemys.Add (this.gameObject);
	

		if (IsPatrolMan) {
			StartPos = transform.parent.transform.parent.transform.position;

			foreach (GameObject pp in PatrolPoint) {
				pp.GetComponent<MeshRenderer> ().enabled = false;
			}

		} else {
			StartPos = transform.parent.transform.position;
		}

		if (IsBoss) {

			this.enabled = false;
			return;
		}

		agent = GetComponent<NavMeshAgent> ();

		agent.speed = DefaultSpeed;
		//line = ll.GetComponent<LineRenderer> ();

	}
	

	void Update () {

		if (MoveTarget.GetComponent<ControllManager> ().IsDead == true) {
			IsFindedHero = false;
		}

		if (IsPatrolMan == true) {

			if (IsFindedHero == true) {

				agent.destination = MoveTarget.transform.position;

				agent.speed = ChaseSpeed;
			} else {

				if (LastTarget != PatrolPoint [NowPatrolPoint]) {

					agent.destination = PatrolPoint [NowPatrolPoint].transform.position;

					agent.speed = DefaultSpeed;

					LastTarget = PatrolPoint [NowPatrolPoint];

				} else {

					agent.destination = PatrolPoint [NowPatrolPoint].transform.position;

					agent.speed = DefaultSpeed;

				}

			}

		} else {

			agent.destination = MoveTarget.transform.position;

		}
		/*
		// 経路取得用のインスタンス作成
		path = new NavMeshPath ();
		// 明示的な経路計算実行
		agent.CalculatePath (PatrolPoint [NowPatrolPoint].transform.position, path);

		// LineRendererで経路描画！
		line.SetVertexCount (path.corners.Length);
		line.SetPositions (path.corners);*/
	}

	public void PatrolTargetChange(){

		NowPatrolPoint++;

		if (NowPatrolPoint >= PatrolPoint.Count) {
			NowPatrolPoint = 0;
		}
	}

	void OnTriggerEnter (Collider col) {

		if(col.CompareTag("PatrolPoint")){

			foreach (GameObject pp in PatrolPoint) {

				if (pp == col.gameObject) {
					PatrolTargetChange ();
					break;
				}

			}
		}

	}
}
