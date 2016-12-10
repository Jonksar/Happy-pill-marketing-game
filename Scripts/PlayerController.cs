using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private SpriteRenderer sprite;
	public float speed = 1.0f;

	// Use this for initialization
	void Start () {
		sprite = this.GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		float inc = Input.GetAxis("Horizontal") * Time.deltaTime * speed;

		sprite.flipX = inc < 0;
		transform.position += new Vector3(inc, 0, 0);
	}
}
