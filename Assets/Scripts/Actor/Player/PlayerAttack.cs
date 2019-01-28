using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	Animator myAnimator;
	Actor actor;

	void Start() {
		actor = GetComponent<Actor>();
		myAnimator = GetComponent<Animator>();
	}

	void Update() {
		if (actor.isBlocked()) {
			return;
		}
		handleInput();
	}

	void handleInput() {
		if (Input.GetButtonDown("Fire1")) {
			prepareAttack();
		}
		else if (Input.GetButtonUp("Fire1")) {
			attack();
		}
	}

	void prepareAttack() {
		myAnimator.SetTrigger("PreparingAttack");
	}

	void attack() {
		actor.block();
		myAnimator.SetTrigger("Attacking");
	}

	void stopAttack() {
		actor.unblock();
	}
}
