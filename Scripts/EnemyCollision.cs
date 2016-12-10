using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour {

	void Start () {
	}
	
	void Update () {
		
	}

	void OnCollisonEnter2D (Collision2D coll) {
		Debug.Log (coll);
	}

	void OnTriggerEnter() {
		Debug.Log ("fdsaf");
	}
}
