using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	[SerializeField] float minTimeToLoad = 0.001f;

	float timeButtonPressed;
	bool isPreparingAttack = false;

	Animator myAnimator;
	Actor actor;
	PlayerMovement playerMovement;
	GameSession gameSession;

	void Start() {
		actor = GetComponent<Actor>();
		playerMovement = GetComponent<PlayerMovement>();
		myAnimator = GetComponent<Animator>();
		gameSession = FindObjectOfType<GameSession>();
	}

	void Update() {
		if (actor.isBlocked()) {
			return;
		}
		handleInput();
		// Attack button still pressed, Start loading a fireball
		if (isPreparingAttack && pressedLong()) {
		}
	}

	void handleInput() {
		if (Input.GetButtonDown("Fire1")) {
			timeButtonPressed = Time.realtimeSinceStartup;
			prepareAttack();
		}
		else if (Input.GetButtonUp("Fire1")) {
			if (isPreparingAttack && gameSession.playerHasBonus("Fireball")) {
				Debug.Log("Throw fireball");
			}
			attack();
		}
	}

	void prepareAttack() {
		isPreparingAttack = true;
		myAnimator.SetTrigger("PreparingAttack");
	}

	void attack() {
		isPreparingAttack = false;
		playerMovement.setDelta(0f);
		actor.block();
		myAnimator.SetTrigger("Attacking");
	}

	bool pressedLong() {
		return Time.realtimeSinceStartup - timeButtonPressed > minTimeToLoad;
	}

	void stopAttack() {
		actor.unblock();
	}
}
