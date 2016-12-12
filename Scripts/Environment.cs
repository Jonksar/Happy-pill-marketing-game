using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {
	
	public Sprite room1;
	public Sprite room2;
	public Sprite room3;
	public Sprite room4;

	public Sprite enemy1j;
	public Sprite enemy2j;
	public Sprite enemy3j;
	public Sprite enemy4j;
	public Sprite enemy1g;
	public Sprite enemy2g;
	public Sprite enemy3g;
	public Sprite enemy4g;

	public Spawner spawnerL;
	public Spawner spawnerR;

	public int pushThisButton = 1;
	private int previous = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			pushThisButton = pushThisButton + 1 % 4;
		}

		if (pushThisButton != previous) {
			if (pushThisButton == 2) {
				ChangeEnvironment (2);
			} else if (pushThisButton == 1) {
				ChangeEnvironment (1);
			} else if (pushThisButton == 3) {
				ChangeEnvironment (3);
			} else if (pushThisButton == 4) {
				ChangeEnvironment (4);
			}
		}
		previous = pushThisButton;
	}

	public void ChangeEnvironment(int i) {
		if (i == 1) {
			GetComponent<SpriteRenderer> ().sprite = room1;
			foreach (Enemy x in Enemy.enemies) {
				//bug: changing everything to demontan now but should check if gamithra or joonatan
				if (x.name == "j")
					x.GetComponent<SpriteRenderer> ().sprite = enemy1j;
				else
					x.GetComponent<SpriteRenderer> ().sprite = enemy1g;
			}
			spawnerL.spriteIndex = 1;
			spawnerR.spriteIndex = 1;

		}
		if (i == 2) {
			GetComponent<SpriteRenderer> ().sprite = room2;
			foreach (Enemy x in Enemy.enemies) {
				if (x.name == "j")
					x.GetComponent<SpriteRenderer> ().sprite = enemy2j;
				else
					x.GetComponent<SpriteRenderer> ().sprite = enemy2g;
			}
			spawnerL.spriteIndex = 2;
			spawnerR.spriteIndex = 2;
		}

		if (i == 3) {
			GetComponent<SpriteRenderer> ().sprite = room3;
			foreach (Enemy x in Enemy.enemies) {
				if (x.name == "j")
					x.GetComponent<SpriteRenderer> ().sprite = enemy3j;
				else
					x.GetComponent<SpriteRenderer> ().sprite = enemy3g;
			}
			spawnerL.spriteIndex = 3;
			spawnerR.spriteIndex = 3;
		}

		if (i == 4) {
			GetComponent<SpriteRenderer> ().sprite = room4;
			foreach (Enemy x in Enemy.enemies) {

				if (x.name == "j")
					x.GetComponent<SpriteRenderer> ().sprite = enemy4j;
				else
					x.GetComponent<SpriteRenderer> ().sprite = enemy4g;
			}
			spawnerL.spriteIndex = 4;
			spawnerR.spriteIndex = 4;
		}
	}
		
}
