using UnityEngine;
using System.Collections;

public class AmbientSound : MonoBehaviour {

	public AnimationCurve fadeCurve;
	public AudioClip[] playList;
	[Range(0.001f, 10f)] public float fadeTime = 10f;
	[Range(0f, 1f)] public float maxVolume = 1f;

	private AudioClip inSound;
	private AudioSource mainSound;
	private AudioSource secondarySound;
	private bool fading;
	private float startedFadingAt;

	void Start(){
		mainSound = getChildGameObject (gameObject, "MainSound").GetComponent<AudioSource> ();
		secondarySound = getChildGameObject (gameObject, "SecondarySound").GetComponent<AudioSource> ();
		fading = true;

		// checks every second if the mainSound is behaving as it should
		InvokeRepeating ("clampVolume", 0f, 1f);
	}

	void Update(){
		Fade ();
	}

	// Use this function from outside to fade another track in 
	public void MakeFade(AudioClip inMusic) {
		inSound = inMusic;
	}

	// Helper function
	static public GameObject getChildGameObject(GameObject fromGameObject, string withName)
	{
		//Author: Isaac Dart, June-13.
		Transform[] ts = fromGameObject.transform.GetComponentsInChildren<Transform> ();
		foreach (Transform t in ts) if (t.gameObject.name == withName) return t.gameObject;
		return null;
	}

	private void Fade() {

		// if we are not currently fading
		if (!fading) {
			// if we get a fade request
			if (inSound != null) {
				startedFadingAt = Time.time; // save the time we started fading
				secondarySound.clip = inSound; // set the clip to secondary audio player
				inSound = null; // reset the in sound to null so we wont do it again
				fading = true;
			}

		} else {
			float curveTime = (Time.time - startedFadingAt) / fadeTime;
			float curveValue = fadeCurve.Evaluate (curveTime);

			mainSound.volume = (1f - curveValue) * maxVolume;
			secondarySound.volume = (curveValue) * maxVolume;

			// if we have finished our fading  
			if (curveTime >= 1f) {
				fading = false;

				// switch mainSound and secondarySound
				mainSound.clip = secondarySound.clip;
				mainSound.volume = maxVolume;
				mainSound.Play ();
				mainSound.time = secondarySound.time;

				secondarySound.clip = null;
				secondarySound.volume = 0f;
				secondarySound.time = 0f;
			}
		}
	}

	private void clampVolume() {
		mainSound.volume = Mathf.Min (maxVolume, mainSound.volume);
	}
}
