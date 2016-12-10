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
	public AudioClip[] hitSfxs;

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

	// Use this for initialization
	void Start () {

		GameObject m_obj = GameObject.Instantiate (fader);
		GameObject sfx_obj = GameObject.Instantiate (serialPlayer);
			
		this.music_player = m_obj.GetComponent<FaderController>();
		this.sfx_player = sfx_obj.GetComponent<SerialPlayerController>();

		m_obj.transform.SetParent (this.transform);
		sfx_obj.transform.SetParent (this.transform);

		InvokeRepeating ("smth", 0f, 21.053f - 2.105f);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void ChangeMusic(int speed, musicType type){
		switch (type) {
		
		case musicType.Happy:
			music_player.MakeFade (this.happyThemes [speed]);
			break;
		
		case musicType.Sad:
			music_player.MakeFade (this.sadThemes [speed]);
			break;

		case musicType.MenuMusic:
			music_player.MakeFade (this.menuThemes [speed]);
			break;
		}
	}
	
	public void smth() {
		this.ChangeMusic (this.musicIndexCounter % this.menuThemes.Length, musicType.MenuMusic);
		this.musicIndexCounter++;
	}

}
