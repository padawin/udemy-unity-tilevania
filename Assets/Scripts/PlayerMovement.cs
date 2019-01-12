using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] float speed = 300f;
	[SerializeField] float jumpInitialVelocity = 10f;

	int jumpsCount = 0;
	int maxJumps = 2;

	Rigidbody2D rb;
	Animator myAnimator;
	GameSession gameSession;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		gameSession = FindObjectOfType<GameSession>();
	}

	// Update is called once per frame
	void Update() {
		handleInput();
		move();
		jump();
		fall();
		updateOrientation();
	}

	float getDelta() {
		return Input.GetAxis("Horizontal") * Time.deltaTime * speed;
	}

	void handleInput() {
		if (Input.GetButtonDown("Jump") && canJump()) {
			startJump();
		}
	}

	bool canJump() {
		return (
			!myAnimator.GetBool("Jumping") && !myAnimator.GetBool("Falling")
			|| gameSession.playerHasBonus("DoubleJump") && jumpsCount < maxJumps
		);
	}

	void startJump() {
		rb.gravityScale = 1f;
		rb.velocity = new Vector2(rb.velocity.x, jumpInitialVelocity);
		myAnimator.SetBool("Falling", false);
		myAnimator.SetBool("Jumping", true);
		jumpsCount++;
	}

	void move() {
		float delta = getDelta();
		rb.velocity = new Vector2(delta, rb.velocity.y);
		myAnimator.SetBool("Running", delta != 0);
		if (rb.velocity.y < 0) {
			startFalling();
		}
	}

	void jump() {
		if (!myAnimator.GetBool("Jumping")) {
			return;
		}

		if (rb.velocity.y < 0) {
			rb.gravityScale = 2.5f;
			myAnimator.SetBool("Jumping", false);
			startFalling();
		}
	}

	void startFalling() {
		myAnimator.SetBool("Falling", true);
	}

	void fall() {
		if (!myAnimator.GetBool("Falling")) {
			return;
		}

		if (rb.velocity.y > -Mathf.Epsilon) {
			rb.gravityScale = 1f;
			myAnimator.SetBool("Falling", false);
			jumpsCount = 0;
		}
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
}
