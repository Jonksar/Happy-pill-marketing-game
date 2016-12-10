using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
	left,
	right
};

// Enemy base class for common behaviour
public class Enemy : MonoBehaviour {
	public float speed;
	public int health;
	public Direction direction;

	void Start () {
		this.GetComponent<SpriteRenderer>().flipX = direction == Direction.left;
	}

	void Update () {
		float slop = 1.0f;
		float inc = Time.deltaTime * speed * (direction == Direction.left ? -1 : 1);
		transform.position += new Vector3(inc, 0, 0);

		if (transform.position.x > -slop && transform.position.x < slop) {
			//Destroy(gameObject);
		}
	}
}
