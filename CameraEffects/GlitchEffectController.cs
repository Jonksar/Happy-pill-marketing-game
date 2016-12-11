using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchEffectController : MonoBehaviour {
	
	[Range(0f, 1f)] public float minIntensity = 1f;
	[Range(0.3f, 2f)] public float maxIntensity = 1f;
	[Range(0f, 2f)] public float changeSpeed = 1f;

	private GlitchEffect gefx;
	// Use this for initialization
	void Start () {
		gefx = gameObject.GetComponent<GlitchEffect> ();
	}
	
	// Update is called once per frame
	void Update () {
		float percentage = Mathf.Sin (Time.realtimeSinceStartup) * Mathf.Sin (Time.realtimeSinceStartup);
		this.gefx.intensity = this.minIntensity + percentage * (maxIntensity - minIntensity);
	}
}
