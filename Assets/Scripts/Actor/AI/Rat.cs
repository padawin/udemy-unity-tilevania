using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : EnemyBackForthMovement {
	[SerializeField] float minDurationWalk = 0f;
	[SerializeField] float maxDurationWalk = 1f;
	[SerializeField] float minDurationStop = 0f;
	[SerializeField] float maxDurationStop = 0f;

	bool isWalking = true;

	new void Start() {
		base.Start();
		StartCoroutine(startWalking());
	}

	IEnumerator startWalking() {
		float timeToWait = Random.Range(minDurationStop, maxDurationStop);
		yield return new WaitForSeconds(timeToWait);
		isWalking = true;
		StartCoroutine(stopWalking());
	}

	IEnumerator stopWalking() {
		float timeToWait = Random.Range(minDurationWalk, maxDurationWalk);
		yield return new WaitForSeconds(timeToWait);
		isWalking = false;
		StartCoroutine(startWalking());
	}

	new void Update() {
		if (isWalking) {
			base.Update();
		}
		else {
			rb.velocity = new Vector2(0f, 0f);
		}
	}
}
