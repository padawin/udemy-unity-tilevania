using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	[SerializeField] Collider2D hitbox;

	Rigidbody2D rb;
	Animator myAnimator;
	GameSession gameSession;
	Player player;

	void Start() {
		player = GetComponent<Player>();
		myAnimator = GetComponent<Animator>();
	}

	void Update() {
		if (player.isBlocked()) {
			return;
		}
		handleInput();
	}

	void handleInput() {
		if (Input.GetButtonDown("Fire1")) {
			attack();
		}
	}

	void attack() {
		player.block();
		myAnimator.SetTrigger("Attacking");
	}

	void stopAttack() {
		player.unblock();
	}
}
