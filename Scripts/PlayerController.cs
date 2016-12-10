using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 1.0f;
	public bool attackingLeft = false;
	public bool attackingRight = false;

	private SpriteRenderer sprite;

	void Start () {
		sprite = this.GetComponent<SpriteRenderer>();
	}

	void Update () {
		float direction = Input.GetAxis("Horizontal");

		if (attackingLeft || attackingRight) {
			return;
		}

		if (direction < 0) {
			attackingLeft = true;
			Invoke("OnAttackEnd", 0.5f);
		}

		if (direction > 0) {
			attackingRight = true;
			Invoke ("OnAttackEnd", 0.5f);
		}
	}

	void OnAttackEnd() {
		attackingLeft = attackingRight = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		GameObject obj = other.gameObject;
		Enemy enemy = obj.GetComponent<Enemy>();

		bool onLeft = obj.transform.position.x < transform.position.x;
		bool onRight = !onLeft;

		if ((onLeft && attackingLeft) || (onRight && attackingRight)) {
			enemy.Hit(10);
		} else {
			enemy.transform.Translate(Vector3.left);
			Destroy(obj);
		}
	}
}
