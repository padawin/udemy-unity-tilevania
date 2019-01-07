using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBackForthMovement : MonoBehaviour {
	[SerializeField] float speed = 100f;
	[SerializeField] float minX;
	[SerializeField] float maxX;
	float target;

	Rigidbody2D rb;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		target = maxX;
	}

	void Update () {
		moveTowardTarget();
		if (turnAround()) {
			updateOrientation();
		}
	}

	void moveTowardTarget() {
		float direction = Mathf.Sign(target - transform.position.x);
		rb.velocity = new Vector2(direction * speed * Time.deltaTime, 0f);
	}

	bool turnAround() {
		if (transform.position.x >= minX && transform.position.x <= maxX) {
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
		float direction = Mathf.Sign(transform.position.x - target);
		transform.localScale = new Vector3(
			direction,
			transform.localScale.y,
			transform.localScale.z
		);
	}
}
