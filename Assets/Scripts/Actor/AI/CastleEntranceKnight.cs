using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleEntranceKnight : MonoBehaviour {
	[SerializeField] float timeBeforeDash = 0f;
	[SerializeField] float timeBeforeStandby = 0f;
	[SerializeField] float dashSpeed = 1f;
	[SerializeField] float dashBoundaryLeft;
	[SerializeField] float dashBoundaryRight;
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

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.name == "Player") {
			myAnimator.SetBool("SeesPlayer", true);
			seesPlayer = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject.name == "Player") {
			myAnimator.SetBool("SeesPlayer", false);
			seesPlayer = false;
		}
	}

	void Update() {
		if (isStandingBy && seesPlayer) {
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
		if (seesPlayer) {
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
