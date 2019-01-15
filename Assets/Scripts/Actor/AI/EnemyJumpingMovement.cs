using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpingMovement : MonoBehaviour {
	[SerializeField] Vector2 jumpStrength;
	[SerializeField] float minX;
	[SerializeField] float maxX;
	[SerializeField] float minTimeBeforeTurn;
	[SerializeField] float minTimeBeforeJump;
	[SerializeField] float maxTimeBeforeJump;
	float lastXPosition;
	float timeBeforeNextJump;
	float direction = 1f;

	bool isJumping = false;

	Rigidbody2D rb;
	Actor actor;

	void Start () {
		lastXPosition = transform.position.x;
		rb = GetComponent<Rigidbody2D>();
		actor = GetComponent<Actor>();
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
		if (!isJumping) {
			StartCoroutine(prepareNextJump());
		}
	}

	IEnumerator prepareNextJump() {
		isJumping = true;
		if (turnAround()) {
			yield return new WaitForSeconds(minTimeBeforeTurn);
			updateOrientation();
		}
		float timeBeforeJump = Random.Range(
			minTimeBeforeJump, maxTimeBeforeJump
		);
		yield return new WaitForSeconds(timeBeforeJump);
		jump();
	}

	bool turnAround() {
		bool isFirstJump = lastXPosition == transform.position.x;
		if (isFirstJump) {
			return isTurnedTowardsClosestBoundary();
		}
		else {
			return wouldJumpOutOfBoundaries();
		}
	}

	bool isTurnedTowardsClosestBoundary() {
		bool turnedLeft = direction == 1f;
		bool closerToLeft = transform.position.x - minX < maxX - transform.position.x;
		return turnedLeft && closerToLeft || !turnedLeft && !closerToLeft;
	}

	bool wouldJumpOutOfBoundaries() {
		float jumpDistance = Mathf.Abs(transform.position.x - lastXPosition);
		float positionAfterJump = transform.position.x - direction * jumpDistance;
		return positionAfterJump < minX || maxX < positionAfterJump;
	}

	void updateOrientation() {
		direction *= -1;
		transform.localScale = new Vector3(
			direction,
			transform.localScale.y,
			transform.localScale.z
		);

	}

	void jump() {
		lastXPosition = transform.position.x;
		rb.velocity = new Vector2(-direction * jumpStrength.x, jumpStrength.y);
	}
}
