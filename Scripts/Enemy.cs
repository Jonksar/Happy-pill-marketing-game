﻿using System.Collections;
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
	public int damage;
	public Direction direction;

	public Sprite dead1;
	public Sprite kicked1;
	public Sprite dead2;
	public Sprite kicked2;
	public Sprite dead3;
	public Sprite kicked3;

	public int env;
	public string demon;

	private Rigidbody2D rigidBody;
	private SpriteRenderer spriteRenderer;
	private SoundManager sounds;
	private int blinking = 0;
	private int blinkFrameDelta = 4;
	private const float hitXImpulse = 0;
	private const float hitYImpulse = 0;

	private GameObject player;

	public static List<Enemy> enemies = new List<Enemy>();

	public void Hit(int damage) {
		health -= damage;

		if (IsDead()) {
			GetComponent<BoxCollider2D>().enabled = false;

			if (sounds != null) {
				sounds.PlayMonsterHitSFX();
			}

			DieImpulse();
		} else {
			sounds.PlayPunchSFX();
		}
	}

	void Start () {
		enemies.Add(this);

		rigidBody = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.flipX = direction == Direction.left;

		GameObject manager = GameObject.Find("SoundManager");
		if (manager != null) {
			sounds = manager.GetComponent<SoundManager>();
		}

		player = GameObject.Find ("Player");
	}

	void OnDestroy() {
		enemies.Remove(this);
	}

	void Update () {
		env = GameObject.Find ("Environment").GetComponent<Environment> ().pushThisButton;

		if (env == 5) {
			speed = 0.8f;
		} else {
			speed = 7;
		}
		if (IsDead()) {
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

		if (env == 1)
			spriteRenderer.sprite = dead1;
		else if (env == 2)
			spriteRenderer.sprite = dead1;
		else if (env == 3)
			spriteRenderer.sprite = dead2;
		else if (env >= 4)
			spriteRenderer.sprite = dead3;
		transform.position = new Vector3 (transform.position.x, transform.position.y - 1.8f, transform.position.z);
		blinkFrameDelta = 3;
		// animation goes here
		Invoke("DeathBlink", 0.5f);


	}

	private void DieImpulse() {


		// Add to kill count
		player.GetComponent<PlayerController>().score.addKill();
		env = GameObject.Find ("Environment").GetComponent<Environment> ().pushThisButton;

		if (env == 1)
			spriteRenderer.sprite = kicked1;
		else if (env == 2)
			spriteRenderer.sprite = kicked1;
		else if (env == 3)
			spriteRenderer.sprite = kicked2;
		else if (env >= 4)
			spriteRenderer.sprite = kicked3;
		float x = direction == Direction.left ? 1 : -1;
		Vector3 impulse = new Vector3(x * hitXImpulse, hitYImpulse, 0);
		rigidBody.AddForce(impulse, ForceMode2D.Impulse);
		Invoke("Die", 0.9f);
	}

	private void Remove() {
		Destroy(gameObject);
	}
}
