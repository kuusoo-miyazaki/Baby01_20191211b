using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGenerator : MonoBehaviour {

	public GameObject Bomb;

	public int MaxCount;

	public float Pos_y;

	public float Pos_x_Max;

	public float Pos_z_Max;

	public List<GameObject> Bombs = new List<GameObject> ();


	public float GenerateSpeed;

	void Start () {
		StartCoroutine ("GenerateBomb");
	}
	

	IEnumerator GenerateBomb () {

		float cnt = 0;

		while (true) {

			cnt += GenerateSpeed;

			if (cnt > 1000) {
				
				if (Bombs.Count < MaxCount) {
					
				GameObject bomb = Instantiate (Bomb, new Vector3 (Random.Range (0, Pos_x_Max), Pos_y, Random.Range (0, Pos_x_Max)), Quaternion.identity) as GameObject;

				bomb.GetComponent<Rigidbody> ().isKinematic = false;

				bomb.transform.Find ("BombBall").transform.Find ("Sensor").GetComponent<BombTimer> ().IsGenerated = true;

				Bombs.Add (bomb);

				}

				cnt = 0;
			}

			yield return new WaitForSeconds (0.01f);

		}

	}
}
