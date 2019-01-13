using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	Animator myAnimator;
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
