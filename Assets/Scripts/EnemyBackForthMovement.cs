using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackForthMovement : MonoBehaviour {
	[SerializeField] int defaultDirection;
	[SerializeField] float speed = 100f;
	[SerializeField] float minX;
	[SerializeField] float maxX;
	float target;

	Rigidbody2D rb;
	Animator myAnimator;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		target = maxX;
		updateOrientation();
	}

	void Update () {
		moveTowardTarget();
		if (turnAround()) {
			updateOrientation();
		}
	}

	void moveTowardTarget() {
		float direction = Mathf.Sign(target - transform.position.x);
		rb.velocity = new Vector2(
			direction * speed * Time.deltaTime, rb.velocity.y
		);
	}

	bool turnAround() {
		if (minX <= transform.position.x && transform.position.x <= maxX) {
			return false;
		}

		target = target == maxX ? minX : maxX;
		transform.position = new Vector2(
			Mathf.Clamp(transform.position.x, minX, maxX),
			transform.position.y
		);

		return true;
	}

	void updateOrientation() {
		float direction = Mathf.Sign(defaultDirection) * Mathf.Sign(transform.position.x - target);
		transform.localScale = new Vector3(
			direction,
			transform.localScale.y,
			transform.localScale.z
		);
		if (myAnimator != null) {
			myAnimator.SetTrigger("Turns");
		}
	}
}
