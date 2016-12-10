using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleRotation : MonoBehaviour {
	private Vector3 rotationVector;

	void Start () {
		
	}

	void Update () {
		/*rotationVector = transform.rotation.eulerAngles;
		rotationVector.x += 0.5f;
		rotationVector.y += 0.5f;
		rotationVector.z += 0.5f;

		Debug.Log (rotationVector);
		Debug.Log (transform.rotation);
		transform.rotation = Quaternion.Euler(rotationVector);
		*/
		transform.Rotate(Vector3.one * 40 * Time.deltaTime);
	}
}
