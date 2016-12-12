using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	private enum State {
		Start,
		TitleFadeIn,
		Blank,
		CrossFade,
		MainMenu
	};

	private State state = State.Start;
	private Image fader;
	private Image background;
	private Text text;
	private float elapsed = 0f;
	private float startDuration = 10.7f;
	private float titleDuration = 10f;
	private float someConstant = 1f; 
	private float blankDuration = 1f;
	private float fadeDuration = 8f;
	private float mainSoundInterval = 5.5f;
	private SoundManager sounds;

	private bool pressedLeft = false;
	private bool pressedRight = false;

	void Start() {
		fader = GameObject.Find("Fader").GetComponent<Image>();
		background = GameObject.Find("Background").GetComponent<Image>();
		text = GameObject.Find("TitleText").GetComponent<Text>();
		sounds = GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}

	void Update() {
		if (state == State.Start && elapsed == 0.0f) {
			sounds.PlaySFX(sounds.glitchSFXs[1], sounds.glitchSFXvolume);
		}

		elapsed += Time.deltaTime;

		if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.D)) {
			sounds.PlayPianoSFX ();
			if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
				this.pressedLeft = true;
			} else {
				this.pressedRight = true;
			}

			if (this.pressedLeft && this.pressedRight) {
				Debug.Log ("Here");
			}
		} 

		switch (state) {
			case State.Start:
				if (elapsed > startDuration) {
					state = State.TitleFadeIn;
					elapsed = 0;
				}
				break;

			case State.TitleFadeIn:
				fader.color = Color.Lerp(Color.black, Color.clear, 1000000 * Mathf.Clamp01(elapsed / titleDuration));

				if (elapsed > titleDuration) {
					state = State.Blank;
					elapsed = 0;
				sounds.PlaySFX(sounds.glitchSFXs[3], sounds.glitchSFXvolume);
				}
				break;

		case State.Blank:
			fader.color = Color.black;

			if (elapsed > blankDuration) {
				state = State.CrossFade;
				elapsed = 0;
				sounds.PlaySFX(sounds.glitchSFXs[5], sounds.glitchSFXvolume);

			}
			break;

			case State.CrossFade:
				text.color = Color.clear;
				background.color = Color.clear;
			fader.color = Color.Lerp(Color.black, Color.clear, Mathf.Clamp01(elapsed / fadeDuration));

				if (elapsed > fadeDuration) {
					state = State.MainMenu;
					elapsed = 0;
					sounds.PlayMentalitySFX();
				}
				break;

			case State.MainMenu:
				if (elapsed > mainSoundInterval) {
					elapsed = 0;
					sounds.PlayMentalitySFX();
				}

				break;
		}
	}

	void ChangeSceneToGame() {
		SceneManager.LoadScene ("GameScene");
	}
}
