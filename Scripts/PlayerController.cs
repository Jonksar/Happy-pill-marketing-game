using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public bool attackingLeft = false;
	public bool attackingRight = false;
	public Sprite idle;
	public Sprite attack1;
	public Sprite attack2;
	public Sprite attack3;
	public Sprite attack4;

	private enum State {
		Idle,
		Attack,
		WallJump
	};

	private SpriteRenderer spriteRenderer;
	private Sprite[] attackSprites = new Sprite[3];

	private const float hitAreaWidth = 4;
	private const float maxDistanceFromCentre = 7;

	private State state = State.Idle;
	private Vector3 startPos;
	private Vector3 desiredPos;
	private float timeRemaining = 0.0f;

	private System.Random rnd = new System.Random();
	private List<Enemy> leftSide = new List<Enemy>();
	private List<Enemy> rightSide = new List<Enemy>();

	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();

		attackSprites[0] = attack1;
		attackSprites[1] = attack2;
		attackSprites[2] = attack3;
		attackSprites [3] = attack4;
	}

	void Update () {
		timeRemaining = Math.Max(0.0f, timeRemaining - Time.deltaTime);
		Vector3 pos = transform.position;
		float xAxis = Input.GetAxis("Horizontal");
		bool left = xAxis < 0;
		bool right = xAxis > 0;

		UpdateLists();

		if (state == State.Idle && (left || right)) {
			List<Enemy> list = left ? leftSide : rightSide;

			if (list.Count > 0) {
				Enemy enemy = list[0];
				list.RemoveAt(0);

				state = State.Attack;
				desiredPos = new Vector3(enemy.transform.position.x, pos.y, pos.z);
				startPos = pos;
				timeRemaining = 0.2f;

				spriteRenderer.flipX = left;
				Invoke("AttackOn", 0.1f);
				enemy.Hit(10);
			}
		}

		if (state == State.Attack) {
			transform.position = Vector3.Lerp(desiredPos, startPos, timeRemaining);

			if (timeRemaining == 0.0f) {
				if (Math.Abs(transform.position.x) > maxDistanceFromCentre) {
					state = State.WallJump;
					desiredPos = Vector3.zero;
					startPos = pos;
					timeRemaining = 0.2f;
					spriteRenderer.flipX = !spriteRenderer.flipX;
				} else {
					state = State.Idle;
				}
			}			
		}

		if (state == State.WallJump) {
			transform.position = Vector3.Lerp(desiredPos, startPos, timeRemaining);

			if (timeRemaining == 0.0f) {
				state = State.Idle;
			}
		}
	}

	void AttackOn() {
		Invoke("OnAttackEnd", 0.1f);
		spriteRenderer.sprite = attackSprites[rnd.Next(0,3)];
		Destroy(GetComponent<BoxCollider2D>());  
		gameObject.AddComponent<BoxCollider2D>();
	}

	void OnAttackEnd() {
		spriteRenderer.sprite = idle;
		attackingLeft = attackingRight = false;
		Destroy(GetComponent<BoxCollider2D>());
		gameObject.AddComponent<BoxCollider2D>();
	}

	private void UpdateLists() {
		leftSide.Clear();
		rightSide.Clear();

		foreach (Enemy enemy in Enemy.enemies) {
			if (!enemy.IsDead() && CloseEnoughToMe(enemy.gameObject.transform.position)) {
				List<Enemy> list = IsLeftOfMe(enemy.gameObject.transform.position) ? leftSide : rightSide;
				list.Add(enemy);
			}
		}

		leftSide.Sort(delegate(Enemy a, Enemy b) {
			return (int)a.gameObject.transform.position.x - (int)b.gameObject.transform.position.x;
		});

		rightSide.Sort(delegate(Enemy a, Enemy b) {
			return (int)b.gameObject.transform.position.x - (int)a.gameObject.transform.position.x;
		});
	}

	private bool IsLeftOfMe(Vector3 pos) {
		return pos.x < transform.position.x;
	}

	private bool CloseEnoughToMe(Vector3 pos) {
		return Mathf.Abs(transform.position.x - pos.x) < hitAreaWidth;
	}
}
