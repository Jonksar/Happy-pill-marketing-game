using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonithraWalk : MonoBehaviour {
	public Sprite walk1;
	public Sprite walk2;
	public Sprite walk3;
	public Sprite walk1mono;
	public Sprite walk2mono;
	public Sprite walk3mono;
	public Sprite walk1true;
	public Sprite walk2true;
	public Sprite walk3true;

	public int env = 1;
	private int prevenv = 0;

	private bool switched;
	void Start () {
		InvokeRepeating ("Animate", 0f, 0.15f);	
	}
	
	void Update () {
	}

	void Animate() {
		env = GameObject.Find ("Environment").GetComponent<Environment> ().pushThisButton;
		Debug.Log (env);
		if (prevenv != env) {
			switched = false;
		}

		if (env == 1) {
			if (GetComponent<SpriteRenderer> ().sprite == walk1) {
				GetComponent<SpriteRenderer> ().sprite = walk2;
			} else if (GetComponent<SpriteRenderer> ().sprite == walk2) {
				GetComponent<SpriteRenderer> ().sprite = walk3;
			} else if (GetComponent<SpriteRenderer> ().sprite == walk3) {
				GetComponent<SpriteRenderer> ().sprite = walk1;
			} else if (!switched) {
				GetComponent<SpriteRenderer> ().sprite = walk1;
				switched = true;
			}
		}
		else if (env == 2) {
			if (GetComponent<SpriteRenderer> ().sprite == walk1) {
				GetComponent<SpriteRenderer> ().sprite = walk2;
			} else if (GetComponent<SpriteRenderer> ().sprite == walk2) {
				GetComponent<SpriteRenderer> ().sprite = walk3;
			} else if (GetComponent<SpriteRenderer> ().sprite == walk3) {
				GetComponent<SpriteRenderer> ().sprite = walk1;
			} else if (!switched) {
				GetComponent<SpriteRenderer> ().sprite = walk1;
				switched = true;
			}
		}
		else if (env == 3) {
			if (GetComponent<SpriteRenderer> ().sprite == walk1mono) {
				GetComponent<SpriteRenderer> ().sprite = walk2mono;
			} else if (GetComponent<SpriteRenderer> ().sprite == walk2mono) {
				GetComponent<SpriteRenderer> ().sprite = walk3mono;
			} else if (GetComponent<SpriteRenderer> ().sprite == walk3mono) {
				GetComponent<SpriteRenderer> ().sprite = walk1mono;
			} else if (!switched) {
				GetComponent<SpriteRenderer> ().sprite = walk1mono;
				switched = true;
			}
		}
		else if (env == 4) {
			if (GetComponent<SpriteRenderer> ().sprite == walk1true) {
				GetComponent<SpriteRenderer> ().sprite = walk2true;
			} else if (GetComponent<SpriteRenderer> ().sprite == walk2true) {
				GetComponent<SpriteRenderer> ().sprite = walk3true;
			} else if (GetComponent<SpriteRenderer> ().sprite == walk3true) {
				GetComponent<SpriteRenderer> ().sprite = walk1true;
			} else if (!switched) {
				GetComponent<SpriteRenderer> ().sprite = walk1true;
				switched = true;
			}
		}
		prevenv = env;
	}
}
