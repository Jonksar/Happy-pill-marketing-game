using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentChanger : MonoBehaviour {
	void Start () {
		RectTransform combo = GameObject.Find("ComboLiteralText").GetComponent<RectTransform>();
		RectTransform rect = GameObject.Find("Canvas").GetComponent<RectTransform>();

		combo.anchoredPosition = new Vector2(rect.sizeDelta.x * 0.45f, rect.sizeDelta.y * 0.8f);
	}
}
