using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BirdState {STANDBY, GO_TO_START, PREPARE_FIRE, FIRE, COOL_DOWN, FIND_PLAYER, GO_TO_PLAYER};

public class Firebird : Observer {
	[SerializeField] FireProjectileSpawner fireProjectileSpawner;
	[SerializeField] Vector2 target;
	[SerializeField] float speedAppears;
	[SerializeField] float speedToPlayer;
	[SerializeField] int direction = -1;

	[SerializeField] float timeBeforeFire = 1f;
	[SerializeField] float timeToCoolDown = 1.5f;
	[SerializeField] float timeFiring = 7f;

	[SerializeField] GameObject[] waypoints;
	[SerializeField] Player player;

	Rigidbody2D rb;

	BirdState state = BirdState.STANDBY;
	Vector2 lastKnownPosition;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		lastKnownPosition = transform.position;
	}

	override public void notify() {
		state = BirdState.GO_TO_START;
	}

	IEnumerator startFiring() {
		yield return new WaitForSeconds(timeBeforeFire);
		state = BirdState.FIRE;
		StartCoroutine(
			fireProjectileSpawner.fire(direction, timeFiring)
		);
	}

	void Update() {
		if (state == BirdState.GO_TO_START) {
			goToTarget(speedAppears);
			if (reachedDestination()) {
				rb.velocity = new Vector2(0f, 0f);
				state = BirdState.PREPARE_FIRE;
				StartCoroutine(startFiring());
			}
		}
		else if (state == BirdState.FIRE) {
			// Finished firing
			if (!fireProjectileSpawner.isFiring()) {
				state = BirdState.COOL_DOWN;
				StartCoroutine(startHeadingTowardsPlayer());
			}
		}
		else if (state == BirdState.FIND_PLAYER) {
			// @TODO go to closest firing point (@TODO define firing points)
			GameObject closest = null;
			foreach (GameObject waypoint in waypoints) {

			}
		}
		else if (state == BirdState.GO_TO_PLAYER) {
			goToTarget(speedToPlayer);
		}
	}

	void goToTarget(float speed) {
		Vector2 direction = target - (Vector2) transform.position;
		float distanceToPosition = direction.magnitude;
		direction.Normalize();
		rb.velocity = direction * speed;
	}

	bool reachedDestination() {
		return (
			Mathf.Sign(target.y - lastKnownPosition.y) != Mathf.Sign(target.y - transform.position.y)
			|| Mathf.Sign(target.x - lastKnownPosition.x) != Mathf.Sign(target.x - transform.position.x)
		);
	}

	IEnumerator startHeadingTowardsPlayer() {
		yield return new WaitForSeconds(timeToCoolDown);
		state = BirdState.FIND_PLAYER;
	}
}
