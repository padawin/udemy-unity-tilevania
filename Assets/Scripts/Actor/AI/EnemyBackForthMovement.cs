using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackForthMovement : MonoBehaviour {
	[SerializeField] float defaultDirection;
	[SerializeField] float xSpeed = 100f;
	[SerializeField] float minX;
	[SerializeField] float maxX;
	float target;

	protected Rigidbody2D rb;
	Actor actor;

	protected void Start() {
		defaultDirection = Mathf.Sign(defaultDirection);
		rb = GetComponent<Rigidbody2D>();
		actor = GetComponent<Actor>();
		target = maxX;
		updateOrientation();
	}

	protected void Update () {
		if (actor.isBlocked()) {
			return;
		}
		moveTowardTarget();
		updateTarget();
		updateOrientation();
	}

	void moveTowardTarget() {
		float direction = Mathf.Sign(target - transform.position.x);
		rb.velocity = new Vector2(
			direction * xSpeed * Time.deltaTime, rb.velocity.y
		);
	}

	void updateTarget() {
		if (minX <= transform.position.x && transform.position.x <= maxX) {
			return;
		}

		target = target == maxX ? minX : maxX;
	}

	void updateOrientation() {
		float direction = defaultDirection * Mathf.Sign(target - transform.position.x);
		transform.localScale = new Vector3(
			direction,
			transform.localScale.y,
			transform.localScale.z
		);
	}
}
