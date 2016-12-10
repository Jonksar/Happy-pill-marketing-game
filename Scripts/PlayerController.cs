using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 1.0f;
	public bool attackingLeft = false;
	public bool attackingRight = false;
	public Sprite idle;
	public Sprite attack1;
	public Sprite attack2;
	public Sprite attack3;

	private SpriteRenderer spriteRenderer;
	private Sprite[] attackSprites = new Sprite[3];
	private float hitAreaWidth = 3;
	private float direction;
	private const float minX = -5;
	private const float maxX = -minX;
	private System.Random rnd = new System.Random();
	private List<Enemy> leftSide = new List<Enemy>();
	private List<Enemy> rightSide = new List<Enemy>();

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		attackSprites[0] = attack1;
		attackSprites[1] = attack2;
		attackSprites[2] = attack3;
	}

	void Update () {
		direction = Input.GetAxis("Horizontal");

		if (direction == 0 || attackingLeft || attackingRight) {
			return;
		}

		UpdateLists();

		attackingLeft = direction < 0;
		attackingRight = direction > 0;
		bool attack = attackingLeft || attackingRight;
		float delta = attackingLeft ? -1 : 1;

		if (attack) {
			List<Enemy> list = attackingLeft ? leftSide : rightSide;

			if (list.Count > 0) {
				Enemy enemy = list[0];
				list.RemoveAt(0);
				setX(enemy.transform.position.x + delta);
				enemy.Hit(10);
			} else {
				attack = false;
				setX(transform.position.x + delta * hitAreaWidth);
				Invoke("OnAttackEnd", 0.35f);
			}
		}

		if (attack) {
			Invoke("AttackOn", 0.15f);
			spriteRenderer.sprite = attack3;
		}

		spriteRenderer.flipX = attackingLeft;
	}

	void AttackOn() {
		Invoke("OnAttackEnd", 0.35f);
		spriteRenderer.sprite = attackSprites[rnd.Next(0,2)];
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

	private void setX(float x) {
		Vector3 pos = transform.position;
		transform.position = new Vector3(Math.Min(maxX, Math.Max(minX, x)), pos.y, pos.z);
	}
}
