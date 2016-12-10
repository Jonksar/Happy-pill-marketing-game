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
	private SpriteRenderer spriteRenderer;
	private SoundManager sounds;
	private int blinking = 0;
	private int blinkFrameDelta = 4;
	private float hitImpulseCoefficient = 3;

	public static List<Enemy> enemies = new List<Enemy>();

	public void Hit(int damage) {
		health -= damage;

		if (IsDead()) {
			GetComponent<BoxCollider2D>().enabled = false;
			blinking = 1;
			sounds.PlayMonsterHitSFX();

			if (Random.value > 0.5f) {
				Die();
			} else {
				DieSlide();
			}
		} else {
			sounds.PlayPunchSFX();
		}
	}

	void Start () {
		enemies.Add(this);

		rigidBody = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.flipX = direction == Direction.left;

		sounds = GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}

	void OnDestroy() {
		enemies.Remove(this);
	}

	void Update () {
		if (blinking > 0) {
			spriteRenderer.enabled = (blinking / blinkFrameDelta) % 2 == 0;
			++blinking;
		}

		if (IsDead()) {
			return;
		}

		float inc = Time.deltaTime * speed * (direction == Direction.left ? -1 : 1);
		transform.position += new Vector3(inc, 0, 0);
	}

	public bool IsDead() {
		return health <= 0;
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
