using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	[Header("Sprites")]
	public Sprite idle;
	public Sprite attack1;
	public Sprite attack2;
	public Sprite attack3;
	public Sprite attack4;
	public Sprite wallslide;
	public Sprite dead;

	[Header("Healthiness")]
	public int health = 10;
	public bool isAlive = true;


	[Header("Score System")]
	public ScoreSystem score;


	private enum State {
		Idle,
		Attack,
		WallJump
	};

	private SpriteRenderer spriteRenderer;
	private Sprite[] attackSprites = new Sprite[4];
	private GameObject leftBox;
	private GameObject rightBox;

	private const float hitAreaWidth = 4;
	private const float maxDistanceFromCentre = 7;

	private State state = State.Idle;
	private Vector3 startPos;
	private Vector3 desiredPos;
	private float percentElapsed = 0.0f;
	private float totalTime;

	private System.Random rnd = new System.Random();
	private List<Enemy> leftSide = new List<Enemy>();
	private List<Enemy> rightSide = new List<Enemy>();

	private SoundManager soundManager;


	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();

		attackSprites[0] = attack1;
		attackSprites[1] = attack2;
		attackSprites[2] = attack3;
		attackSprites [3] = attack4;

		this.soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager>();

		leftBox = GameObject.Find("LeftGuideBox");
		rightBox = GameObject.Find("RightGuideBox");
	}

	void Update () {
		if (totalTime > 0.0f) {
			percentElapsed = Math.Min(1.0f, percentElapsed + Time.deltaTime / totalTime);
		}

		Vector3 pos = transform.position;
		bool left = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
		bool right = (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && !left;

		UpdateLists();

		if (state == State.Idle && (left || right)) {
			List<Enemy> list = left ? leftSide : rightSide;

			if (list.Count > 0) {
				Enemy enemy = list [0];
				list.RemoveAt (0);

				state = State.Attack;
				desiredPos = new Vector3 (enemy.transform.position.x, pos.y, pos.z);
				startPos = pos;
				setAnimLength (0.2f);

				spriteRenderer.flipX = left;
				AttackOn ();
				enemy.Hit (10);
			} else {
				score.ComboFail ();
				this.health -= 1; 
			}
		}

		if (state == State.Attack) {
			transform.position = EaseIn(startPos, desiredPos, percentElapsed);

			if (percentElapsed == 1.0f) {
				if (Math.Abs(transform.position.x) > maxDistanceFromCentre) {
					state = State.WallJump;
					spriteRenderer.sprite = wallslide;
					desiredPos = new Vector3(0, -0.6f, 0);
					startPos = pos;
					setAnimLength(0.4f);
					spriteRenderer.flipX = !spriteRenderer.flipX;
				} else {
					OnAttackEnd();
				}
			}
		} else if (state == State.WallJump) {
			transform.position = EaseIn(startPos, desiredPos, percentElapsed);

			if (percentElapsed == 1.0f) {
				OnAttackEnd();
			}
		}

		if (health < 0 && isAlive) {
			Die ();
		}

		UpdateGuideBoxes();
	}

	private void UpdateGuideBoxes() {
		Vector3 pos = gameObject.transform.position;
		Vector2 playerPos = Camera.main.WorldToScreenPoint(pos);
		Vector2 leftReachPos = Camera.main.WorldToScreenPoint(pos + Vector3.left * hitAreaWidth);

		float boxWidth = playerPos.x - leftReachPos.x;
		leftBox.GetComponent<RectTransform>().sizeDelta = new Vector2(boxWidth * 3f, 40.0f);
		rightBox.GetComponent<RectTransform>().sizeDelta = new Vector2(boxWidth * 3f , 40.0f);

		leftBox.GetComponent<Image>().color = leftSide.Count > 0 ? Color.red : Color.grey;
		rightBox.GetComponent<Image>().color = rightSide.Count > 0 ? Color.red : Color.grey;
	}

	void AttackOn() {
		soundManager.PlayPunchSFX ();
		spriteRenderer.sprite = attackSprites[rnd.Next(0,3)];
		Destroy(GetComponent<BoxCollider2D>());  
		gameObject.AddComponent<BoxCollider2D>();
	}

	void OnAttackEnd() {
		spriteRenderer.sprite = idle;
		Destroy(GetComponent<BoxCollider2D>());
		gameObject.AddComponent<BoxCollider2D>();
		state = State.Idle;
		setAnimLength(0);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Enemy enemy = collider.gameObject.GetComponent<Enemy>();

		if (!enemy.IsDead()) {
			if (state == State.WallJump) {
				enemy.Hit(1000);
			} else {
				enemy.Hit (1000);
				score.ComboFail ();
				health -= 1;
			}
		}
	}

	private void Die() {
		Debug.Log("Die");
		isAlive = false;
		spriteRenderer.sprite = dead;
		soundManager.PlayPlayerDeath ();
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

	private void setAnimLength(float time) {
		totalTime = time;
		percentElapsed = 0;
	}

	private Vector3 EaseIn(Vector3 start, Vector3 end, float amount) {
		Vector3 delta = end - start;
		return start + delta * amount * amount;
	}
}
