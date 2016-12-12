using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DikMove : MonoBehaviour {
	private bool dik2;
	void Start () {
		if (transform.position.y == 2.5f) {
			dik2 = true;
		}	
	}
	
	void Update () {
		if (!dik2) {
			transform.position = new Vector3 (transform.position.x - 0.07f, Mathf.Sin ((System.DateTime.Now.Ticks / 100000 % 31416) / 30.0f) * 1, transform.position.z);
		} else {
			transform.position = new Vector3 (transform.position.x + 0.07f, Mathf.Sin ((System.DateTime.Now.Ticks / 100000 % 31416) / 30.0f) * 1, transform.position.z);
			GetComponent<SpriteRenderer>().flipX = true;

		}
	}
}
