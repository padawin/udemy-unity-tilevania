using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRain : MonoBehaviour {
	[SerializeField] FireProjectile fireProjectilePrefab;

	[SerializeField] int nbFireballsUp;
	[SerializeField] float timeBetweenFireballsUp;
	[SerializeField] float ttlFireballUp;
	[SerializeField] float projectileSpeedUp;

	[SerializeField] int nbFireballs;
	[SerializeField] float timeBetweenFireballs;
	[SerializeField] float minPosFireBallX;
	[SerializeField] float maxPosFireBallX;
	[SerializeField] float posFireBallY;

	List<FireProjectile> fireballs;

	EvilLord evilLord;

	void Start() {
		fireballs = new List<FireProjectile>();
		evilLord = GetComponent<EvilLord>();
	}

	public void fire() {
		StartCoroutine(throwFireballUp());
		StartCoroutine(throwFireballs());
	}


	IEnumerator throwFireballUp() {
		for (int f = 0; f < nbFireballsUp; ++f) {
			yield return new WaitForSeconds(timeBetweenFireballsUp);
			float posX = Random.Range(
				transform.position.x - 1.5f, transform.position.x + 1.5f
			);
			FireProjectile projectile = Instantiate(
				fireProjectilePrefab,
				new Vector3(posX, transform.position.y, transform.position.z),
				Quaternion.identity
			);
			projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(
				0f, projectileSpeedUp
			);
			Destroy(projectile, ttlFireballUp);
		}
	}

	IEnumerator throwFireballs() {
		yield return new WaitForSeconds(
			nbFireballsUp * ttlFireballUp * timeBetweenFireballsUp
		);
		for (int f = 0; f < nbFireballs; ++f) {
			float posX = Random.Range(minPosFireBallX, maxPosFireBallX);
			fireballs.Add(Instantiate(
				fireProjectilePrefab,
				new Vector3(posX, posFireBallY, transform.position.z),
				Quaternion.identity
			));
			yield return new WaitForSeconds(timeBetweenFireballs);
		}
		StartCoroutine(backToIdle());
	}

	IEnumerator backToIdle() {
		yield return new WaitUntil(
			() => allFireballsDestroyed()
		);
		fireballs.Clear();
		evilLord.setIdle();
	}

	bool allFireballsDestroyed() {
		foreach (var fireball in fireballs) {
			if (fireball != null) {
				return false;
			}
		}
		return true;
	}
}
