using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTowardsPlayer : MonoBehaviour {
	[SerializeField] float timeBeforeDash = 0f;
	[SerializeField] float timeBeforeStandby = 0f;
	[SerializeField] float dashSpeed = 1f;
	[SerializeField] float dashBoundaryLeft;
	[SerializeField] float dashBoundaryRight;
	[SerializeField] Collider2D playerDetector;
	float direction = -1f;
	// states
	bool isStandingBy = true;
	bool isDashing = false;

	bool seesPlayer = false;

	Animator myAnimator;
	Rigidbody2D rb;
	GameObject player;

	void Start() {
		player = FindObjectOfType<Player>().gameObject;
		myAnimator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}

	bool isSeeingPlayer() {
		bool nowSeesPlayer = playerDetector.IsTouchingLayers(LayerMask.GetMask("Player"));
		if (nowSeesPlayer != seesPlayer) {
			myAnimator.SetBool("SeesPlayer", nowSeesPlayer);
		}
		seesPlayer = nowSeesPlayer;
		return seesPlayer;
	}

	void Update() {
		if (isStandingBy && isSeeingPlayer()) {
			isStandingBy = false;
			turnToward(player);
			StartCoroutine(dash());
		}
		else if (isDashing) {
			if (!reachedBoundaries()) {
				rb.velocity = new Vector2(direction * dashSpeed * Time.deltaTime, rb.velocity.y);
			}
			else {
				isDashing = false;
				keepInBounds();
				StartCoroutine(stopDashing());
			}
		}
	}

	void turnToward(GameObject player) {
		direction = Mathf.Sign(player.transform.position.x - transform.position.x);
		transform.localScale = new Vector3(
			-direction, transform.localScale.y, transform.localScale.z
		);
	}

	IEnumerator dash() {
		yield return new WaitForSeconds(timeBeforeDash);
		if (isSeeingPlayer()) {
			myAnimator.SetBool("Dashes", true);
			isDashing = true;
		}
		else {
			isStandingBy = true;
		}
	}

	bool reachedBoundaries() {
		return transform.position.x < dashBoundaryLeft ||
			dashBoundaryRight < transform.position.x;
	}

	void keepInBounds() {
		transform.position = new Vector2(
			Mathf.Clamp(transform.position.x, dashBoundaryLeft, dashBoundaryRight),
			transform.position.y
		);
	}

	IEnumerator stopDashing() {
		rb.velocity = new Vector2(0f, 0f);
		myAnimator.SetBool("Dashes", false);
		yield return new WaitForSeconds(timeBeforeStandby);
		isStandingBy = true;
	}
}
