using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
	[SerializeField] float minTimeToLoad = 0.001f;
	[SerializeField] float minTimeToPrepareFireball = 1f;
	[SerializeField] ParticleSystem fireballPrepared;
	[SerializeField] ParticleSystem fireballReady;
	[SerializeField] GameObject fireball;

	float timeButtonPressed;
	bool isPreparingAttack = false;
	bool isPreparingFireball = false;

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
		prepareFireball();
	}

	void handleInput() {
		if (Input.GetButtonDown("Fire1")) {
			timeButtonPressed = Time.realtimeSinceStartup;
			prepareAttack();
		}
		else if (Input.GetButtonUp("Fire1")) {
			if (isPreparingAttack && gameSession.playerHasBonus("Fireball")) {
				throwFireball();
			}
			attack();
		}
	}

	void prepareAttack() {
		isPreparingAttack = true;
		myAnimator.SetTrigger("PreparingAttack");
	}

	void throwFireball() {
		if (isFireballReady()) {
			GameObject fb = Instantiate(
				fireball, transform.position, Quaternion.identity
			);
			fb.GetComponent<Fireball>().setDirection(playerMovement.getDirection());
		}
		isPreparingFireball = false;
		fireballReady.gameObject.SetActive(false);
		fireballPrepared.gameObject.SetActive(false);
	}

	void attack() {
		isPreparingAttack = false;
		if (!playerMovement.isInTheAir()) {
			playerMovement.setDelta(0f);
		}
		actor.block();
		myAnimator.SetTrigger("Attacking");
	}

	void prepareFireball() {
		if (!gameSession.playerHasBonus("Fireball")) {
			return;
		}

		bool fireballPreparedActive = fireballPrepared.gameObject.activeSelf,
			 fireballReadyActive = fireballReady.gameObject.activeSelf;
		if (isPreparingFireball && isFireballReady()) {
			if (!fireballReadyActive) {
				fireballPrepared.gameObject.SetActive(false);
				fireballReady.gameObject.SetActive(true);
			}
		}
		else if (isPreparingAttack && pressedLong() && !fireballPreparedActive) {
			fireballPrepared.gameObject.SetActive(true);
			isPreparingFireball = true;
		}
	}

	bool isFireballReady() {
		return Time.realtimeSinceStartup - timeButtonPressed > minTimeToPrepareFireball;
	}

	bool pressedLong() {
		return Time.realtimeSinceStartup - timeButtonPressed > minTimeToLoad;
	}

	void stopAttack() {
		actor.unblock();
	}
}
