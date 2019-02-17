using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BirdState {STANDBY, GO_TO_START, FIRE, GO_TO_PLAYER};

public class Firebird : Observer {
	[SerializeField] FireProjectileSpawner fireProjectileSpawner;
	[SerializeField] Vector2 startPosition;
	[SerializeField] float speed;
	[SerializeField] int direction = -1;

	[SerializeField] float timeFiring = 7f;

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
		// @TODO serialize that
		yield return new WaitForSeconds(1f);
		state = BirdState.FIRE;
	}

	void Update() {
		if (state == BirdState.GO_TO_START) {
			goToStartPosition();
		}
		else if (state == BirdState.STANDBY) {
			rb.velocity = new Vector2(0f, 0f);
		}
		else if (state == BirdState.FIRE) {
			if (!fireProjectileSpawner.isFiring()) {
				StartCoroutine(
					fireProjectileSpawner.fire(direction, timeFiring)
				);
				state = BirdState.STANDBY;
			}
		}
	}

	void goToStartPosition() {
		Vector2 direction = startPosition - (Vector2) transform.position;
		float distanceToPosition = direction.magnitude;
		direction.Normalize();
		rb.velocity = direction * speed;
		if (reachedDestination()) {
			state = BirdState.STANDBY;
			StartCoroutine(startFiring());
		}
	}

	bool reachedDestination() {
		return Mathf.Sign(startPosition.y - lastKnownPosition.y) != Mathf.Sign(startPosition.y - transform.position.y);
	}
}
