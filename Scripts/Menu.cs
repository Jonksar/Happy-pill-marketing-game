using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	private Image mainImage;
	private float elapsed = 0;
	private float startDuration = 2;
	private float titleDuration = 9;
	private float blankDuration = 2;
	private float fadeDuration = 2;
	private float mainSoundInterval = 10;
	private SoundManager sounds;

	void Start() {
		fader = GameObject.Find("Fader").GetComponent<Image>();
		mainImage = GameObject.Find("MainImage").GetComponent<Image>();
		sounds = GameObject.Find("SoundManager").GetComponent<SoundManager>();
	}

	void Update() {
		if (elapsed == 0.0f) {
			sounds.PlaySFX(sounds.glitchSFXs[1]);
		}

		elapsed += Time.deltaTime;

		switch (state) {
			case State.Start:
				if (elapsed > startDuration) {
					state = State.TitleFadeIn;
					elapsed = 0;
				}
				break;

			case State.TitleFadeIn:
				fader.color = Color.Lerp(Color.black, Color.clear, Mathf.Clamp01(elapsed / titleDuration));

				if (elapsed > titleDuration) {
					state = State.Blank;
					elapsed = 0;
					sounds.PlaySFX(sounds.glitchSFXs[3]);
				}
				break;

			case State.Blank:
				fader.color = Color.black;

				if (elapsed > blankDuration) {
					state = State.CrossFade;
					elapsed = 0;
					sounds.PlayGlitchSFX();
				}
				break;

			case State.CrossFade:
				mainImage.color = Color.Lerp(Color.clear, Color.white, Mathf.Clamp01(elapsed / fadeDuration));

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
}
