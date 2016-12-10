using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class DemontanHovering : MonoBehaviour {
	public float hoverRadius;
	public float hoverSpeed;

	private float hover = 0;
	private bool direction = false;
	private float fixedY;
	private float hoverOffset;

	void Start () {
		fixedY = transform.position.y;
		hoverOffset = Random.value;
		//hoverRadius = 0.1f;
		//hoverSpeed = 0.001f;
	}

	void Update () {
		long milliseconds = System.DateTime.Now.Ticks / 100000 % 31416;
		transform.position = new Vector3 (transform.position.x, fixedY + Mathf.Sin(hoverOffset + milliseconds / 30.0f) * 0.4f, transform.position.z);
		/*if (hover >= hoverRadius) {
			direction = false;
		} else if (hover <= 0) {
			direction = true;
		}

		if (direction) {
			hover += hoverSpeed;
		} else {
			hover -= hoverSpeed;
		}

		transform.Translate (Vector3.up * hover);*/
	}
}
