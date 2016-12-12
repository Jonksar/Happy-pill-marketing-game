using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {


	[Range(0f, 5f)] public float comboTimeout = 2f;

	[Header("The metrics")]
	public int killCount = 0;
	public float score = 0;
	public float multiplier = 1f;

	public AnimationCurve failSizeCurve;
	public GameObject ComboLiteralText;
	public GameObject canvas;

	[Header("Instansiate references")]
	public GameObject TextPrefab;


	private float lastHitTime = 0f;
	private GameObject player;
	private SoundManager sounds;
	private GameObject ComboNumberText;
	private RectTransform ComboLiteralRectT;

	private Vector3 comboLiteralRotation;
	private Vector3 comboLiteralScale;

	private GameObject failLiteralText;
	private RectTransform failLiteralRectT;

	public float failTime = 1000000f;


	// Use this for initialization
	void Start () {
		sounds = GameObject.Find ("SoundManager").GetComponent<SoundManager>();
		player = GameObject.Find ("Player");

		ComboLiteralRectT = ComboLiteralText.GetComponent<RectTransform> ();

		failLiteralText = Instantiate (TextPrefab);
		failLiteralText.GetComponent<Text> ().color = Color.red;
		failLiteralText.transform.SetParent (canvas.transform);
		failLiteralText.GetComponent<Text> ().text = "FAIL!";
		failLiteralRectT = failLiteralText.GetComponent<RectTransform> ();
		failLiteralRectT.position = ComboLiteralRectT.position;
		failLiteralRectT.sizeDelta = ComboLiteralRectT.sizeDelta;

		lastHitTime = Time.time;
	}

	void Update() {
		failTime += Time.deltaTime;

		ComboLiteralRectT.localScale = Vector3.Lerp (ComboLiteralRectT.localScale, comboLiteralScale, 10f * Time.deltaTime);
		ComboLiteralRectT.eulerAngles = Vector3.Lerp (ComboLiteralRectT.eulerAngles, comboLiteralRotation, 10f * Time.deltaTime);  

		if (Time.time - lastHitTime > comboTimeout) {
			ComboFail ();
		}

		Debug.Log (failTime);
		failLiteralRectT.localScale = 3f *Vector3.one * failSizeCurve.Evaluate(Mathf.Clamp01(failTime)); 
	}

	private void ComboTextUpdate() {
		float val = Random.value;
		comboLiteralRotation = new Vector3 (0f, 0f, Random.value * 15f);
		comboLiteralScale = new Vector3(1f + multiplier * val, 1f + multiplier * val, 1f);

		string comboText;

		if (killCount > 4) {
			comboText = "Combo! ";

			if (killCount > 15) {
				comboText = "Insanity! ";
			} else if (killCount > 30) {
				comboText = "Maddness! ";
			} else if (killCount > 75) {
				comboText = "Psycho! ";
			}

			ComboLiteralText.GetComponent<Text>().text = comboText + killCount.ToString();

		} else { 
			ComboLiteralText.GetComponent<Text>().text = "";
		}

	}

	private void PlayComboSounds() {
		if (killCount == 1) {
			Debug.Log ("First Blood");
			sounds.FirstBlood ();
		} else if (killCount == 10) {
			Debug.Log ("10 kills");
		} else if (killCount == 20) {
			Debug.Log ("Intense fuckery");
			sounds.IntenseFightingMusic ();
		}
	}
	public void addKill(int amount=1) {
		this.killCount += amount;
		multiplier += 0.01f;
		lastHitTime = Time.time;

		this.PlayComboSounds ();
		ComboTextUpdate ();
		this.score += 1 * multiplier;

	}

	public void ComboFail() {
		failTime = 0f; 
		Debug.Log ("Combo fail");
		this.multiplier = 1f;
		killCount = 0;
	}
}
