using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchEffectController : MonoBehaviour {
	[Range(0f, 1f)] public float minIntensity = 1f;
	[Range(0.3f, 2f)] public float maxIntensity = 1f;
	[Range(0f, 2f)] public float changeSpeed = 1f;

	private GlitchEffect gefx;
	private bool oscillate = true;
	private float deltaGlitch;
	private float remainingTime;

	void Start () {
		gefx = gameObject.GetComponent<GlitchEffect> ();
	}
	
	void Update () {
		if (oscillate) {
			float percentage = Mathf.Sin (Time.realtimeSinceStartup) * Mathf.Sin (Time.realtimeSinceStartup);
			this.gefx.intensity = this.minIntensity + percentage * (maxIntensity - minIntensity);
		} else {
			makeGlitchOut ();
		}
	}
		
	public void GlitchOut(float aimGlitch, float time) {
		this.deltaGlitch = (aimGlitch - this.gefx.intensity) / time;
		this.remainingTime = time;
		this.oscillate = false;
	}

	private void makeGlitchOut() {
		if (this.remainingTime < 0f) {
			// oscillate = true;
		} else {
			this.gefx.intensity += this.deltaGlitch * Time.deltaTime;
			this.remainingTime -= Time.deltaTime;
		}
	}
}
