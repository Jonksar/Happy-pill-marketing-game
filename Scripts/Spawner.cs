using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject obj;
	public Direction direction;
	public float spawnRate = 1f;
	public int spriteIndex = 1;

	private float timesSpawned = 0f;

	public void Start() {
		Invoke ("Spawn", Random.value * 0.5f);
	}

	private void Spawn() {
		timesSpawned++;
		GameObject o = Instantiate(obj, transform.position, transform.rotation);
		Enemy enemy = o.GetComponent<Enemy>();
		o.GetComponent<SpriteChange>().ChangeSprite (spriteIndex);
		enemy.direction = direction;

		spawnRate = 0.2f + 0.09f * Mathf.Sqrt(timesSpawned);

		if (GameObject.Find ("Environment").GetComponent<Environment> ().HappyPillTime) {
			Invoke ("Spawn", Random.value * (7f - 6f) + 6f);
		} else {
		        Invoke("Spawn", 1f / (spawnRate + Random.value * 0.333f * spawnRate));
		}
	}
}
