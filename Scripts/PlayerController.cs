using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	[Header("Sprites")]
	public Sprite idle;
	public Sprite idlemono;
	public Sprite idletrue;
	public Sprite attack1;
	public Sprite attack2;
	public Sprite attack3;
	public Sprite attack4;
	public Sprite attack1mono;
	public Sprite attack2mono;
	public Sprite attack3mono;
	public Sprite attack4mono;
	public Sprite attack1true;
	public Sprite attack2true;
	public Sprite attack3true;
	public Sprite attack4true;
	public Sprite wallslide;
	public Sprite wallslidemono;
	public Sprite wallslidetrue;
	public Sprite dead;
	public Sprite deadmono;
	public Sprite deadtrue;
	public Sprite gidle;
	public Sprite gidlemono;
	public Sprite gidletrue;
	public Sprite gattack1;
	public Sprite gattack2;
	public Sprite gattack3;
	public Sprite gattack4;
	public Sprite gattack1mono;
	public Sprite gattack2mono;
	public Sprite gattack3mono;
	public Sprite gattack4mono;
	public Sprite gattack1true;
	public Sprite gattack2true;
	public Sprite gattack3true;
	public Sprite gattack4true;
	public Sprite gwallslide;
	public Sprite gwallslidemono;
	public Sprite gwallslidetrue;
	public Sprite gdead;
	public Sprite gdeadmono;
	public Sprite gdeadtrue;

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
	private GameObject canvas;
	private GameObject leftBox;
	private GameObject rightBox;
	private Sprite[] attackSpritesMono = new Sprite[4];
	private Sprite[] attackSpritesTrue = new Sprite[4];


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
	private int environment;
	private int player;

	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		player = rnd.Next(0, 2);
		Debug.Log (player);
		if (player == 0) {
			attackSprites [0] = attack1;
			attackSprites [1] = attack2;
			attackSprites [2] = attack3;
			attackSprites [3] = attack4;
			attackSpritesMono [0] = attack1mono;
			attackSpritesMono [1] = attack2mono;
			attackSpritesMono [2] = attack3mono;
			attackSpritesMono [3] = attack4mono;
			attackSpritesTrue [0] = attack1true;
			attackSpritesTrue [1] = attack2true;
			attackSpritesTrue [2] = attack3true;
			attackSpritesTrue [3] = attack4true;
			spriteRenderer.sprite = idle;
		} else {
			attackSprites [0] = gattack1;
			attackSprites [1] = gattack2;
			attackSprites [2] = gattack3;
			attackSprites [3] = gattack4;
			attackSpritesMono [0] = gattack1mono;
			attackSpritesMono [1] = gattack2mono;
			attackSpritesMono [2] = gattack3mono;
			attackSpritesMono [3] = gattack4mono;
			attackSpritesTrue [0] = gattack1true;
			attackSpritesTrue [1] = gattack2true;
			attackSpritesTrue [2] = gattack3true;
			attackSpritesTrue [3] = gattack4true;
			spriteRenderer.sprite = gidle;
		}


		this.soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager>();

		leftBox = GameObject.Find("LeftGuideBox");
		rightBox = GameObject.Find("RightGuideBox");
		canvas = GameObject.Find("Canvas");
	}

	void Update () {
		environment = GameObject.Find ("Environment").GetComponent<Environment> ().pushThisButton;
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
					if (player == 0) {
						if (environment == 1)
							spriteRenderer.sprite = wallslide;
						else if (environment == 2)
							spriteRenderer.sprite = wallslide;
						else if (environment == 3)
							spriteRenderer.sprite = wallslidemono;
						else if (environment == 4)
							spriteRenderer.sprite = wallslidetrue;
					} else {
						if (environment == 1)
							spriteRenderer.sprite = gwallslide;
						else if (environment == 2)
							spriteRenderer.sprite = gwallslide;
						else if (environment == 3)
							spriteRenderer.sprite = gwallslidemono;
						else if (environment == 4)
							spriteRenderer.sprite = gwallslidetrue;
					}
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
		Vector3 playerBottom = Camera.main.WorldToScreenPoint(new Vector3(pos.x, pos.y - spriteRenderer.sprite.bounds.extents.y, pos.z));

		leftBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x - 100.0f, playerBottom.y);
		rightBox.GetComponent<RectTransform>().anchoredPosition = new Vector2(pos.x + 100.0f, playerBottom.y);

		leftBox.GetComponent<Image>().color = leftSide.Count > 0 ? Color.red : Color.black;
		rightBox.GetComponent<Image>().color = rightSide.Count > 0 ? Color.red : Color.black;
	}

	void AttackOn() {
		soundManager.PlayPunchSFX ();
		if (environment == 1)
			spriteRenderer.sprite = attackSprites[rnd.Next(0,4)];
		else if (environment == 2)
			spriteRenderer.sprite = attackSprites[rnd.Next(0,4)];
		else if (environment == 3)
			spriteRenderer.sprite = attackSpritesMono[rnd.Next(0,4)];
		else if (environment == 4)
			spriteRenderer.sprite = attackSpritesTrue[rnd.Next(0,4)];
		
		Destroy(GetComponent<BoxCollider2D>());  
		gameObject.AddComponent<BoxCollider2D>();
	}

	void OnAttackEnd() {
		if (player == 0) {
			if (environment == 1)
				spriteRenderer.sprite = idle;
			else if (environment == 2)
				spriteRenderer.sprite = idle;
			else if (environment == 3)
				spriteRenderer.sprite = idlemono;
			else if (environment == 4)
				spriteRenderer.sprite = idletrue;
		} else {
			if (environment == 1)
				spriteRenderer.sprite = gidle;
			else if (environment == 2)
				spriteRenderer.sprite = gidle;
			else if (environment == 3)
				spriteRenderer.sprite = gidlemono;
			else if (environment == 4)
				spriteRenderer.sprite = gidletrue;
		}
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
		if (player == 0) {
			if (environment == 1)
				spriteRenderer.sprite = dead;
			else if (environment == 2)
				spriteRenderer.sprite = dead;
			else if (environment == 3)
				spriteRenderer.sprite = deadmono;
			else if (environment == 4)
				spriteRenderer.sprite = deadtrue;
		} else {
			if (environment == 1)
				spriteRenderer.sprite = gdead;
			else if (environment == 2)
				spriteRenderer.sprite = gdead;
			else if (environment == 3)
				spriteRenderer.sprite = gdeadmono;
			else if (environment == 4)
				spriteRenderer.sprite = gdeadtrue;
		}
		//transform.position = new Vector3 (transform.position.x, transform.position.y+0.2f, transform.position.z);
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
