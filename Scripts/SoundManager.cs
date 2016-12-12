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

	[Header("Menu Music")]
	public AudioClip menuMusic1;
	public AudioClip menuMusic2;

	[Header("Escalation Music")]
	public AudioClip escalationMusic1;
	public AudioClip escalationMusic2;

	[Header("First Blood")]
	public AudioClip firstBloodMusic;

	[Header("Fighting Music")]
	public AudioClip toFightingTransitionMusic;
	public AudioClip fighting1Music;
	public AudioClip fighting2Music;

	[Header("Chill Pill Music")]
	public AudioClip ChillPillMusic;

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

	void Awake() {
		DontDestroyOnLoad (this.gameObject);
	}

	public void ChangeTo(AudioClip audio) {
		music_player.MakeFade(audio);
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

	public void PlayPianoSFX(float pitch) {
		this.sfx_player.PlaySFXwPitch (this.pianoSFX, 1f, pitch);
	}

	public void MenuMusic() {
		CancelInvoke ();
		InvokeRepeating ("IntroMusic_", 0f, menuMusic2.length - this.fadeTime);
	}

	public void FirstBlood() {
		CancelInvoke ();
		InvokeRepeating ("FirstBloodMusic_", escalationMusic1.length, escalationMusic2.length - this.fadeTime);
	}

	public void EscalationMusic() {
		//this.ChangeTo ();
		CancelInvoke ();
		InvokeRepeating ("EscalationMusic_", escalationMusic1.length, escalationMusic2.length - this.fadeTime);
	}

	public void IntenseFightingMusic() {
		//this.ChangeTo ();
		CancelInvoke ();
		//InvokeRepeating ();
	}

	private void EscalationMusic_() {
		this.ChangeTo (escalationMusic2);
	}

	private void MenuMusic_() {
		this.ChangeTo (menuMusic2);
	}

	private void FirstBloodMusic_() {
		this.ChangeTo (firstBloodMusic);
	}

	private void FightingMusic_() {
		this.ChangeTo (fighting2Music);
	}
}
