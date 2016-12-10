using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject obj;
	public Direction direction;
	public float spawnRateSeconds;

	public void Start() {
		InvokeRepeating("Spawn", spawnRateSeconds, spawnRateSeconds);
	}

	private void Spawn() {
		GameObject o = Instantiate(obj, transform.position, transform.rotation);
		Enemy enemy = o.GetComponent<Enemy>();

		enemy.direction = direction;
	}
}
