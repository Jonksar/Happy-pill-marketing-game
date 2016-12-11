using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonithraWalk : MonoBehaviour {
	public Sprite walk1;
	public Sprite walk2;
	public Sprite walk3;

	void Start () {
		InvokeRepeating ("Animate", 0f, 0.17f);	
	}
	
	void Update () {
	}

	void Animate() {
		if (GetComponent<SpriteRenderer> ().sprite == walk1) {
			GetComponent<SpriteRenderer> ().sprite = walk2;
		} else if (GetComponent<SpriteRenderer> ().sprite == walk2) {
			GetComponent<SpriteRenderer> ().sprite = walk3;
		} else if (GetComponent<SpriteRenderer> ().sprite == walk3) {
			GetComponent<SpriteRenderer> ().sprite = walk1;
		}
	}
}
