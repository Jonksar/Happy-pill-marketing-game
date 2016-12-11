using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour {

	public Sprite room1;
	public Sprite room2;
	public Sprite enemy1;
	public Sprite enemy2;
	public Spawner spawnerL;
	public Spawner spawnerR;

	public bool pushThisButton = false;
	private bool previous = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (pushThisButton != previous) {
			if (pushThisButton) {
				ChangeEnvironment (2);
			} else {
				ChangeEnvironment (1);
			}
		}
		previous = pushThisButton;
	}

	public void ChangeEnvironment(int i) {
		if (i == 1) {
			GetComponent<SpriteRenderer> ().sprite = room1;
			foreach (Enemy x in Enemy.enemies) {
				x.GetComponent<SpriteRenderer> ().sprite = enemy1;
			}
			spawnerL.spriteIndex = 1;
			spawnerR.spriteIndex = 1;

		}
		if (i == 2) {
			GetComponent<SpriteRenderer> ().sprite = room2;
			foreach (Enemy x in Enemy.enemies) {
				x.GetComponent<SpriteRenderer> ().sprite = enemy2;
			}
			spawnerL.spriteIndex = 2;
			spawnerR.spriteIndex = 2;


		}

	}	
}
