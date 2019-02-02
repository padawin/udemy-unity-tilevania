using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitResponse : HitResponse {
	[SerializeField] Vector2 hitProjection;
	Rigidbody2D rb;
	Actor actor;
	Animator myAnimator;
	PlayerMovement playerMovement;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		actor = GetComponent<Actor>();
		myAnimator = GetComponent<Animator>();
		playerMovement = GetComponent<PlayerMovement>();
	}

	override public void run(GameObject source) {
		float hitDirection = Mathf.Sign(
			transform.position.x - source.transform.position.x
		);
		rb.velocity = new Vector2(hitDirection * hitProjection.x, hitProjection.y);
		playerMovement.blockMovements();
		myAnimator.SetTrigger("BeingHit");
		actor.block();
	}

	override public void end() {
		playerMovement.unblockMovements();
		actor.unblock();
	}
}
