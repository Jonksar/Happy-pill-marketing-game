using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public GameObject obj;
	public Direction direction;
	public float spawnMinRateSeconds;
	public float spawnMaxRateSeconds;
	public int spriteIndex = 1;

	public void Start() {
		Spawn();
	}

	private void Spawn() {
		GameObject o = Instantiate(obj, transform.position, transform.rotation);
		Enemy enemy = o.GetComponent<Enemy>();
		o.GetComponent<SpriteChange>().ChangeSprite (spriteIndex);
		enemy.direction = direction;

		Invoke("Spawn", Random.value * (spawnMaxRateSeconds - spawnMinRateSeconds) + spawnMinRateSeconds);
	}
}
