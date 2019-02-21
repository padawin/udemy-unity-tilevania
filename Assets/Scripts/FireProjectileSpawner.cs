using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileSpawner : MonoBehaviour {
	[SerializeField] GameObject fireProjectilePrefab;
	[SerializeField] float minTimeBetweenFireball = 0f;
	[SerializeField] float maxTimeBetweenFireball = 1f;
	[SerializeField] Vector2 minFireballDistance;
	[SerializeField] Vector2 maxFireballDistance;

	bool firing = false;

	public bool isFiring() {
		return firing;
	}

	public IEnumerator fire(float direction, float duration) {
		StartCoroutine(stopFiring(duration));
		firing = true;
		while (firing) {
			createFireball(direction);
			yield return new WaitForSeconds(Random.Range(minTimeBetweenFireball, maxTimeBetweenFireball));
		}
	}

	void createFireball(float direction) {
		var fireball = Instantiate(
			fireProjectilePrefab, transform.position, Quaternion.identity
		);
		fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(
			direction * Random.Range(minFireballDistance.x, maxFireballDistance.x),
			Random.Range(minFireballDistance.y, maxFireballDistance.y)
		);
	}

	IEnumerator stopFiring(float timeFiring) {
		yield return new WaitForSeconds(timeFiring);
		firing = false;
	}
}
