using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {
	public Sprite floor1;
	public Sprite room1;
	public Sprite floor2;
	public Sprite room2;

	public bool pushThisButton;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (pushThisButton)
			ChangeEnvironment (2);
	}

	public void ChangeEnvironment(int i) {
		if (i == 1) {
			transform.Find ("room1").GetComponent<SpriteRenderer> ().sprite = room1;
			transform.Find ("floor1").GetComponent<SpriteRenderer> ().sprite = floor1;
		}
		if (i == 2) {
			transform.Find ("room1").GetComponent<SpriteRenderer> ().sprite = room2;
			transform.Find ("floor1").GetComponent<SpriteRenderer> ().sprite = floor2;
		}

	}	
}
