using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemontanHovering : MonoBehaviour {
	private float originY;
	private float hoverOffset;

	void Start () {
		originY = transform.position.y;
		hoverOffset = Random.value;
	}

	void Update() {
		if (!GetComponent<Enemy>().IsDead()) {
			Vector3 pos = transform.position;
			long milliseconds = System.DateTime.Now.Ticks / 100000 % 31416;
			transform.position = new Vector3(pos.x, originY + Mathf.Sin (hoverOffset + milliseconds / 30.0f) * 0.4f, pos.z);
		}
	}
}
