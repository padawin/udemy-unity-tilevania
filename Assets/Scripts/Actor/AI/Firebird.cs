using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BirdState {STANDBY, GO, PREPARE_FIRE, FIRE, COOL_DOWN, FIND_PLAYER};

public class Firebird : Observer {
	[SerializeField] FireProjectileSpawner fireProjectileSpawner;
	[SerializeField] GameObject target;
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
	float speed;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	override public void notify() {
		state = BirdState.GO;
		speed = speedAppears;
	}

	IEnumerator startFiring() {
		yield return new WaitForSeconds(timeBeforeFire);
		state = BirdState.FIRE;
		StartCoroutine(
			fireProjectileSpawner.fire(direction, timeFiring)
		);
	}

	void Update() {
		if (state == BirdState.GO) {
			goToTarget(speed);
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
			findClosestWaypoint();
		}
	}

	void goToTarget(float speed) {
		transform.position = Vector2.MoveTowards(
			transform.position, target.transform.position, speed * Time.deltaTime
		);
	}

	bool reachedDestination() {
		return target.transform.position == transform.position;
	}

	IEnumerator startHeadingTowardsPlayer() {
		yield return new WaitForSeconds(timeToCoolDown);
		state = BirdState.FIND_PLAYER;
	}

	void findClosestWaypoint() {
		GameObject closest = null;
		float closestDist = 0f;
		foreach (GameObject waypoint in waypoints) {
			float distance = (waypoint.transform.position - player.transform.position).magnitude;
			if (closest == null || distance < closestDist) {
				closest = waypoint;
				closestDist = distance;
			}
		}
		target = closest;
		speed = speedToPlayer;
		state = BirdState.GO;
	}
}
