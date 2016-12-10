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

	private Rigidbody2D rigidBody;
	private SpriteRenderer renderer;
	private int blinking = 0;
	private int blinkFrameDelta = 4;
	private float hitImpulseCoefficient = 3;

	public void Hit(int damage) {
		health -= damage;

		if (health <= 0) {
			GetComponent<BoxCollider2D>().enabled = false;
			blinking = 1;

			if (false && Random.value > 0.5f) {
				Die();
			} else {
				DieSlide();
			}
		}
	}

	void Start () {
		GetComponent<SpriteRenderer>().flipX = direction == Direction.left;
		rigidBody = GetComponent<Rigidbody2D>();
		renderer = GetComponent<SpriteRenderer>();
	}

	void Update () {
		if (blinking > 0) {
			renderer.enabled = (blinking / blinkFrameDelta) % 2 == 0;
			++blinking;
		}

		if (health <= 0) {
			return;
		}

		float inc = Time.deltaTime * speed * (direction == Direction.left ? -1 : 1);
		transform.position += new Vector3(inc, 0, 0);
	}

	private void DeathBlink() {
		blinkFrameDelta = 2;
		Invoke("Remove", 0.5f);
	}

	private void Die() {
		rigidBody.simulated = false;
		blinkFrameDelta = 3;
		// animation goes here
		Invoke("DeathBlink", 0.5f);
	}

	private void DieSlide() {
		Vector3 impulse = direction == Direction.left ? Vector3.right : Vector3.left;

		rigidBody.AddForce(impulse * hitImpulseCoefficient, ForceMode2D.Impulse);
		// animation goes here
		Invoke ("Die", 0.9f);
	}

	private void Remove() {
		Destroy(gameObject);
	}
}
