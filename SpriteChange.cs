﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour {
	public Sprite room1Sprite;
	public Sprite room2Sprite;
	public Sprite room3Sprite;
	public Sprite room4Sprite;
	void Start () {
		
	}
	
	void Update () {
		
	}

	public void ChangeSprite(int i) {
		if (i == 1) {
			GetComponent<SpriteRenderer> ().sprite = room1Sprite;
		}	
		if (i == 2) {
			GetComponent<SpriteRenderer> ().sprite = room2Sprite;
		}
		if (i == 3) {
			GetComponent<SpriteRenderer> ().sprite = room3Sprite;
		}
		if (i == 4) {
			GetComponent<SpriteRenderer> ().sprite = room4Sprite;
		}
	}
}
