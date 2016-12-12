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
	public AudioClip[] playerDeathSFX;

	[Range(0f, 1f)] public float punchSFXvolume = 1f;
	[Range(0f, 1f)] public float monsterHitSFXvolume = 1f;
	[Range(0f, 1f)] public float playerHitSFXvolume = 1f;
	[Range(0f, 1f)] public float glitchSFXvolume = 1f;
	[Range(0f, 1f)] public float mentalitySFXvolume = 1f;
	[Range(0f, 1f)] public float musicSFXvolume = 1f;
	[Range(0f, 1f)] public float playerDeathSFXVolume = 1f;
	[Range(0f, 1f)] public float comboSFXvolume = 1f;

	[Header("Music (sorted by speed)")]
	public AudioClip[] happyThemes;
	public AudioClip[] sadThemes;

	[Header("Menu Music")]
	public AudioClip menuMusic1;
	public AudioClip menuMusic2;

	[Header("Escalation Music")]
	public AudioClip escalationMusic1;
	public AudioClip escalationMusic21;
	public AudioClip escalationMusic22;

	[Header("First Blood")]
	public AudioClip firstBloodMusic;

	[Header("Fighting Music")]
	public AudioClip toFightingTransitionMusic;
	public AudioClip fighting1Music;
	public AudioClip fighting2Music;

	[Header("Combo SFX")]
	public AudioClip comboSFX;
	public AudioClip InsanitySFX;
	public AudioClip MadnessSFX;
	public AudioClip PsychoSFX;

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
		PlaySFX(punchSFXs[Random.Range(0, punchSFXs.Length - 1)], punchSFXvolume);

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


	public void PlayPlayerDeath() {
		PlaySFX (playerDeathSFX[Random.Range(0, playerDeathSFX.Length - 1)], playerDeathSFXVolume);
	}

	public void MenuMusic() {
		CancelInvoke ();
		InvokeRepeating ("MenuMusic_", 0f, menuMusic2.length - this.fadeTime);
	}

	public void FirstBlood() {
		CancelInvoke ();
		InvokeRepeating ("FirstBloodMusic_", 0f, escalationMusic22.length - this.fadeTime);
	}

	public void EscalationMusic() {
		this.ChangeTo (escalationMusic1);
		CancelInvoke ();
		Invoke ("EscalationMusic22_", escalationMusic1.length - fadeTime);
		InvokeRepeating ("EscalationMusic21_", escalationMusic1.length + escalationMusic22.length - 2 * fadeTime, escalationMusic22.length - this.fadeTime);
	}

	public void IntenseFightingMusic() {
		this.ChangeTo (toFightingTransitionMusic);
		CancelInvoke ();
		Invoke("FightingMusic2_", fighting1Music.length - this.fadeTime);
		InvokeRepeating("FightingMusic3_", fighting1Music.length + fighting2Music.length - 2 * fadeTime, fighting2Music.length - this.fadeTime);
	}

	public void playComboSFX() {
		PlaySFX (comboSFX, comboSFXvolume);
	}


	public void playInsanitySFX() {
		PlaySFX (InsanitySFX, comboSFXvolume);
	}


	public void playMadnessSFX() {
		PlaySFX (MadnessSFX, comboSFXvolume);
	}


	public void playPsychoSFX() {
		PlaySFX (PsychoSFX, comboSFXvolume);
	}

	private void EscalationMusic21_() {
		this.ChangeTo (escalationMusic22);
	}

	private void MenuMusic_() {
		this.ChangeTo (menuMusic2);
	}

	private void FirstBloodMusic_() {
		this.ChangeTo (firstBloodMusic);
	}

	private void FightingMusic2_() {
		this.ChangeTo (fighting1Music);
	}

	private void FightingMusic3_() {
		this.ChangeTo (fighting2Music);
	}

}
