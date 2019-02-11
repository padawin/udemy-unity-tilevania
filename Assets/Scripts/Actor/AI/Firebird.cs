using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BirdState {STANDBY, GO_TO_START, ACTIVE};

public class Firebird : Observer {
	[SerializeField] Vector2 startPosition;
	[SerializeField] float speed;

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

	void Update() {
		if (state == BirdState.GO_TO_START) {
			goToStartPosition();
		}
		else if (state == BirdState.ACTIVE) {
			rb.velocity = new Vector2(0f, 0f);
		}
	}

	void goToStartPosition() {
		Vector2 direction = startPosition - (Vector2) transform.position;
		float distanceToPosition = direction.magnitude;
		direction.Normalize();
		rb.velocity = direction * speed;
		if (reachedDestination()) {
			state = BirdState.ACTIVE;
		}
	}

	bool reachedDestination() {
		return Mathf.Sign(startPosition.y - lastKnownPosition.y) != Mathf.Sign(startPosition.y - transform.position.y);
	}
}
