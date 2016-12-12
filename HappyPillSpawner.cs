﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappyPillSpawner : MonoBehaviour {
	public bool pillSpawned = false;
	public GameObject obj;
	// Use this for initialization
	void Start () {
		Invoke ("SpawnPill", 3f);
		InvokeRepeating ("SpawnPill", 30f, 20f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && pillSpawned) {
			GameObject.Find ("Environment").GetComponent<Environment> ().pushThisButton = 5;
			GameObject.Find ("Happy Pill").GetComponent<Image> ().color = Color.clear;
			GameObject.Find ("Text").GetComponent<Text> ().color = Color.clear;
		}
	}

	public void SpawnPill() {
		if (!pillSpawned) {
			pillSpawned = true;
			GameObject.Find ("Happy Pill").GetComponent<Image> ().color = Color.white;
			GameObject.Find ("Text").GetComponent<Text> ().color = Color.black;
		}
	}
}
