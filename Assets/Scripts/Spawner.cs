using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public float spawnTime = 5f;
	public float spawnDelay = 3f;
	public GameObject[] enemies;

	void Start () {
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}

	void Spawn () {
		int enemyIndex = Random.Range(0, enemies.Length);
		if ( isInsideTheFrustrum() ) {
			Instantiate(enemies[enemyIndex], transform.position, transform.rotation);
		}
	}

	protected bool isInsideTheFrustrum() {
		Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
		return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
	}

}
