using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleRotation : MonoBehaviour {
	void Update () {
		transform.Rotate(Vector3.one * 40 * Time.deltaTime);
	}
}
