using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] float speed = 300f;
	[SerializeField] float jumpInitialVelocity = 10f;
	[SerializeField] float minVerticalSpeedToJump = Mathf.Epsilon;

	Rigidbody2D rb;
	Animator myAnimator;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
	}

	float getDelta() {
		return Input.GetAxis("Horizontal") * Time.deltaTime * speed;
	}

	void OnCollisionEnter2D(Collision2D collider) {
		myAnimator.SetBool("Falling", false);
	}

	void jump() {
		if (Mathf.Abs(rb.velocity.y) > minVerticalSpeedToJump) {
			return;
		}

		rb.gravityScale = 1f;
		if (Input.GetButtonDown("Jump")) {
			rb.velocity = new Vector2(rb.velocity.x, jumpInitialVelocity);
			myAnimator.SetBool("Jumping", true);
		}
	}

	void fall() {
		if (rb.velocity.y < 0 && !myAnimator.GetBool("Falling")) {
			if (myAnimator.GetBool("Jumping")) {
				rb.gravityScale = 2.5f;
			}
			myAnimator.SetBool("Jumping", false);
			myAnimator.SetBool("Running", false);
			myAnimator.SetBool("Falling", true);
		}
	}

	void move() {
		float delta = getDelta();
		rb.velocity = new Vector2(getDelta(), rb.velocity.y);
		myAnimator.SetBool("Running", delta != 0);
	}

	void updateOrientation() {
		float direction = Mathf.Sign(rb.velocity.x);
		if (rb.velocity.x == 0 || direction == transform.localScale.x) {
			return;
		}
		transform.localScale = new Vector3(
			Mathf.Sign(rb.velocity.x),
			transform.localScale.y,
			transform.localScale.z
		);
	}

	// Update is called once per frame
	void Update() {
		move();
		jump();
		fall();
		updateOrientation();
	}
}
