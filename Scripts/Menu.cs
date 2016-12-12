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
		TutorialLeft,
		TutorialRight,
		GlitchOut
	};

	private State state = State.Start;
	private Image fader;
	private Image background;
	private Text text;
	private Text tutorialText;
	private float elapsed = 0;
	private float startDuration = 10.7f;
	private float titleDuration = 10f;
	private float someConstant = 1f; 
	private float blankDuration = 1f;
	private float fadeDuration = 8f;
	private float glitchOutInterval = 2;

	private SoundManager sounds;

	//private bool pressedLeft = false;
	//private bool pressedRight = false;

	void Start() {
		fader = GameObject.Find("Fader").GetComponent<Image>();
		background = GameObject.Find("Background").GetComponent<Image>();
		text = GameObject.Find("TitleText").GetComponent<Text>();
		tutorialText = GameObject.Find("TutorialText").GetComponent<Text>();
		sounds = GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}

	void Update() {
		if (state == State.Start && elapsed == 0.0f) {
			sounds.PlaySFX(sounds.glitchSFXs[1], sounds.glitchSFXvolume);
		}

		elapsed += Time.deltaTime;

		/*
		if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.D)) {			
			if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
				sounds.PlayPianoSFX (1f);
				this.pressedLeft = true;
			} else {
				sounds.PlayPianoSFX (1.777f);
				this.pressedRight = true;
			}
		} 
		*/

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
					state = State.TutorialLeft;
					tutorialText.text = "Press left arrow key";
					sounds.PlayMentalitySFX();
				}
				break;

			case State.TutorialLeft:
				if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
					state = State.TutorialRight;
					sounds.PlayPianoSFX(1f);
					tutorialText.text = "Press right arrow key";
				}
				break;

			case State.TutorialRight:
				if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
					state = State.GlitchOut;
					elapsed = 0;
					sounds.PlayPianoSFX(1.777f);
					tutorialText.text = "Use the same keys in game";
					//sounds.PlayGlitchSFX();
				    Camera.main.GetComponent<GlitchEffectController>().GlitchOut (4.0f, glitchOutInterval);
				}
				break;

			case State.GlitchOut: 
				if (elapsed > glitchOutInterval) {
					sounds.MenuMusic ();
					SceneManager.LoadScene (1);
				}
				break;
		}
	}
}
