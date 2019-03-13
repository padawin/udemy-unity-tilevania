using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EvilLordState {Idle, PickAttack, Fireball, Dash};

public class EvilLord : Observer {
	[SerializeField] float minTimeBeforeAttack = 0;
	[SerializeField] float maxTimeBeforeAttack = 0;

	EvilLordState state = EvilLordState.Idle;
	bool active = false;

	Slice slice;
	FireRain fireRain;

	ActorHealth actorHealth;

	override public void notify() {
		active = true;
		actorHealth.setActive(true);
	}
	void Start () {
		actorHealth = GetComponent<ActorHealth>();
		actorHealth.setActive(false);
		slice = GetComponent<Slice>();
		fireRain = GetComponent<FireRain>();
	}

	void Update () {
		if (!active) {
			return;
		}

		if (state == EvilLordState.Idle) {
			state = EvilLordState.PickAttack;
			StartCoroutine(preparePickingAttack());
		}
	}

	IEnumerator preparePickingAttack() {
		float timeBeforeAttack = Random.Range(
			minTimeBeforeAttack, maxTimeBeforeAttack
		);
		yield return new WaitForSeconds(timeBeforeAttack);
		attack();
	}

	void attack() {
		float proba = Random.Range(0, 100);
		if (proba < 50) {
			throwFireballs();
		}
		else {
			sliceAttack();
		}
	}

	void throwFireballs() {
		fireRain.fire();
	}

	void sliceAttack() {
		slice.slice();
	}

	public void setIdle() {
		state = EvilLordState.Idle;
	}
}
