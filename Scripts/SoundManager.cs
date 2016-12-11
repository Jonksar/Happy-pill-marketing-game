using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum musicType {
	Happy,
	Sad,
	MenuMusic
};


public class SoundManager : MonoBehaviour {
	[Header("General attributes")]
	public AnimationCurve fadeCurve;
	[Range(0.001f, 10f)] public float fadeTime = 10f;
	[Range(0f, 1f)] public float maxVolume = 1f;

	[Header("Sound Effects")]
	public AudioClip[] punchSFXs;
	public AudioClip[] monsterHitSFXs;
	public AudioClip[] playerHitSFXs;
	public AudioClip[] glitchSFXs;
	public AudioClip[] mentalitySFXs;

	[Header("Music (sorted by speed)")]
	public AudioClip[] happyThemes;
	public AudioClip[] sadThemes;
	public AudioClip[] menuThemes;

	[Header("Instantiate properties")]
	public GameObject fader;
	public GameObject serialPlayer;

	private FaderController music_player;
	private SerialPlayerController sfx_player;

	public int musicIndexCounter = 0;

	void Start() {
		GameObject m_obj = GameObject.Instantiate (fader);
		GameObject sfx_obj = GameObject.Instantiate (serialPlayer);
			
		this.music_player = m_obj.GetComponent<FaderController>();
		this.sfx_player = sfx_obj.GetComponent<SerialPlayerController>();

		m_obj.transform.SetParent (this.transform);
		sfx_obj.transform.SetParent (this.transform);
	}

	public void ChangeTo(AudioClip audio) {
		music_player.MakeFade(audio);
	}

	public void ChangeMusic(int speed, musicType type){
		AudioClip[] clips =
			type == musicType.Happy ? this.happyThemes :
			type == musicType.Sad ? this.sadThemes : this.menuThemes;

		ChangeTo(clips[speed]);
	}

	public void smth() {
		this.ChangeMusic (this.musicIndexCounter % this.menuThemes.Length, musicType.MenuMusic);
		this.musicIndexCounter++;
	}

	public void PlaySFX(AudioClip audio) {
		this.sfx_player.PlaySFX(audio);
	}

	public void PlayPunchSFX() {
		PlaySFX(monsterHitSFXs[Random.Range(0, punchSFXs.Length - 1)]);
	}

	public void PlayMonsterHitSFX() {
		PlaySFX(monsterHitSFXs [Random.Range (0, monsterHitSFXs.Length - 1)]);
	}

	public void PlayPlayerHitSFX() {
		PlaySFX(playerHitSFXs [Random.Range (0, playerHitSFXs.Length - 1)]);
	}

	public void PlayGlitchSFX() {
		PlaySFX(glitchSFXs[Random.Range(0, glitchSFXs.Length - 1)]);
	}

	public void PlayMentalitySFX() {
		PlaySFX(mentalitySFXs[Random.Range(0, mentalitySFXs.Length - 1)]);
	}
}
