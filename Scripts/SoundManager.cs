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
	public AudioClip pianoSFX;

	[Range(0f, 1f)] public float punchSFXvolume = 1f;
	[Range(0f, 1f)] public float monsterHitSFXvolume = 1f;
	[Range(0f, 1f)] public float playerHitSFXvolume = 1f;
	[Range(0f, 1f)] public float glitchSFXvolume = 1f;
	[Range(0f, 1f)] public float mentalitySFXvolume = 1f;
	[Range(0f, 1f)] public float musicSFXvolume = 1f;

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

	public void PlaySFX(AudioClip audio, float volume) {
		this.sfx_player.PlaySFX(audio, volume);
	}

	public void PlayPunchSFX() {
		PlaySFX(monsterHitSFXs[Random.Range(0, punchSFXs.Length - 1)], punchSFXvolume);

	}

	public void PlayMonsterHitSFX() {
		PlaySFX(monsterHitSFXs [Random.Range (0, monsterHitSFXs.Length - 1)], monsterHitSFXvolume);
	}

	public void PlayPlayerHitSFX() {
		PlaySFX(playerHitSFXs [Random.Range (0, playerHitSFXs.Length - 1)], playerHitSFXvolume);
	}

	public void PlayGlitchSFX() {
		PlaySFX(glitchSFXs[Random.Range(0, glitchSFXs.Length - 1)], glitchSFXvolume);
	}

	public void PlayMentalitySFX() {
		PlaySFX(mentalitySFXs[Random.Range(0, mentalitySFXs.Length - 1)], mentalitySFXvolume);
	}

	public void PlayPianoSFX() {
		PlaySFX (pianoSFX, 1f);
	}
}
