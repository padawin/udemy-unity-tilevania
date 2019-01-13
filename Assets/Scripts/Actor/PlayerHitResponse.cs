using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitResponse : HitResponse {
	[SerializeField] Vector2 hitProjection;
	Rigidbody2D rb;
	Player player;
	Animator myAnimator;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		player = GetComponent<Player>();
		myAnimator = GetComponent<Animator>();
	}

	override public void run (GameObject source) {
		float hitDirection = Mathf.Sign(
			transform.position.x - source.transform.position.x
		);
		rb.velocity = new Vector2(hitDirection * hitProjection.x, hitProjection.y);
		myAnimator.SetTrigger("BeingHit");
		player.block();
	}
}
