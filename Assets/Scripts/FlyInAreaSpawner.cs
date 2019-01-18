using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlyInAreaSpawner : MonoBehaviour {
	[SerializeField] Vector2 destination;
	[SerializeField] bool looping = false;
	[SerializeField] int maxNbEnemies = 1;
	[SerializeField] float minTimeBeforeSpawn = 0;
	[SerializeField] float maxTimeBeforeSpawn = 0;
	[SerializeField] List<GameObject> enemyPrefabs = new List<GameObject>();
	[SerializeField] float speed;
	[SerializeField] float minX;
	[SerializeField] float maxX;
	[SerializeField] float minY;
	[SerializeField] float maxY;
	List<GameObject> enemies = new List<GameObject>();

	// Use this for initialization
	IEnumerator Start () {
		do {
			yield return StartCoroutine(spawnEnemies());
		} while (looping);
	}

	IEnumerator spawnEnemies() {
		removeNullElements();
		while (enemies.Count < maxNbEnemies) {
			yield return new WaitForSeconds(getTimeBeforeNextEnemy());
			enemies.Add(spawnEnemy());
		}
	}

	float getTimeBeforeNextEnemy() {
		return Random.Range(minTimeBeforeSpawn, maxTimeBeforeSpawn);
	}

	GameObject spawnEnemy() {
		var enemy = Instantiate(
			enemyPrefabs[Random.Range(0, enemyPrefabs.Count)],
			transform.position,
			Quaternion.identity
		);
		FlyInArea comp = enemy.GetComponent<FlyInArea>();
		comp.setDestination(destination);
		comp.setArea(minX, maxX, minY, maxY);
		comp.setSpeed(speed);
		return enemy;
	}

	void removeNullElements() {
		for (int i = enemies.Count - 1; i >= 0; i--) {
			if (enemies[i] == null) {
				enemies[i] = enemies[enemies.Count - 1];
				enemies.RemoveAt(enemies.Count-1);
			}
		}
	}
}
