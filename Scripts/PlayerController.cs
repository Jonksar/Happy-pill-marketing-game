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
	private float direction;

	private SpriteRenderer spriteRenderer;
	private Sprite[] attackSprites = new Sprite[3];
	System.Random rnd = new System.Random();

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		attackSprites[0] = attack1;
		attackSprites[1] = attack2;
		attackSprites[2] = attack3;
	}

	void Update () {
		direction = Input.GetAxis("Horizontal");

		if (attackingLeft || attackingRight) {
			return;
		}

		if (direction < 0) {
			attackingLeft = true;
			Invoke ("AttackOn", 0.15f);
			spriteRenderer.sprite = attack3;
			if (!spriteRenderer.flipX) {
				spriteRenderer.flipX = true;
			}
		}

		if (direction > 0) {
			attackingRight = true;
			Invoke ("AttackOn", 0.15f);
			spriteRenderer.sprite = attack3;
			if (spriteRenderer.flipX) {
				spriteRenderer.flipX = false;
			} 
		}
	}

	void AttackOn() {
		if (direction > 0) {
			Invoke ("OnAttackEnd", 0.35f);
			spriteRenderer.sprite = attackSprites[rnd.Next(1,2)];
			Destroy(GetComponent<BoxCollider2D>());  
			gameObject.AddComponent<BoxCollider2D>();

		} else {
			Invoke("OnAttackEnd", 0.35f);
			spriteRenderer.sprite = attackSprites[rnd.Next(1,2)];
			Destroy(GetComponent<BoxCollider2D>());  
			gameObject.AddComponent<BoxCollider2D>();
		}
			
	}

	void OnAttackEnd() {
		spriteRenderer.sprite = idle;
		attackingLeft = attackingRight = false;
		Destroy(GetComponent<BoxCollider2D>());  
		gameObject.AddComponent<BoxCollider2D>();
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
