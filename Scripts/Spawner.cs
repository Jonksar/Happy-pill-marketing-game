using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject obj;
	public Direction direction;
	public float spawnRateSeconds;

	void Start () {
		InvokeRepeating("Spawn", spawnRateSeconds, spawnRateSeconds);
	}

	void Spawn() {
		//Enemy enemy = (Enemy)Instantiate(obj);
		//enemy.direction = direction;
		GameObject o = Instantiate(obj, transform.position, transform.rotation);
		Enemy test = o.GetComponent<Enemy>();
		//test.transform.position = transform;
		test.direction = direction;
	}
}
