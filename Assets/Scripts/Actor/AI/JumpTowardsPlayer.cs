using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTowardsPlayer : MonoBehaviour {
	[SerializeField] Vector2 jumpStrength;
	[SerializeField] float defaultDirection;
	float direction = 1f;

	bool isJumping = false;

	Rigidbody2D rb;
	Actor actor;
	GameObject player;

	void Start () {
		defaultDirection = Mathf.Sign(defaultDirection);
		rb = GetComponent<Rigidbody2D>();
		actor = GetComponent<Actor>();
		player = FindObjectOfType<Player>().gameObject;
	}

	void OnCollisionEnter2D(Collision2D collider) {
		if (collider.gameObject.name == "Ground") {
			isJumping = false;
		}
	}

	void Update () {
		if (actor.isBlocked()) {
			return;
		}
		if (player != null && !isJumping) {
			prepareNextJump();
			jump();
		}
	}

	void prepareNextJump() {
		if (!isFacingPlayer()) {
			turnAround();
		}
		updateOrientation();
	}

	bool isFacingPlayer() {
		return direction == Mathf.Sign(player.transform.position.x - transform.position.x);
	}

	void turnAround() {
		direction *= -1;
	}

	void updateOrientation() {
		transform.localScale = new Vector3(
			direction * defaultDirection,
			transform.localScale.y,
			transform.localScale.z
		);

	}

	void jump() {
		isJumping = true;
		rb.velocity = new Vector2(direction * jumpStrength.x, jumpStrength.y);
	}
}
