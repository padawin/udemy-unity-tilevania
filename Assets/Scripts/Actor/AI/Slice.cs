using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SliceState {Idle, Shake, Move};

public class Slice : MonoBehaviour {
	[SerializeField] float timeShaking = 0;
	[SerializeField] Lightning lightningPrefab;
	float speed = 0.1f; // How fast it shakes
	float amount = 0.3f; // How much it shakes

	SliceState state = SliceState.Idle;
	bool active = false;
	Vector3 originalPosition;
	Lightning lightning;

	EvilLord evilLord;

	void Start() {
		evilLord = GetComponent<EvilLord>();
	}

	public void slice() {
		originalPosition = transform.position;
		state = SliceState.Shake;
		StartCoroutine(move());

	}

	void Update() {
		if (state == SliceState.Shake) {
			shake();
		}
		else if (state == SliceState.Move && lightning == null) {
			// Lightning disappeared
			state = SliceState.Idle;
			evilLord.setIdle();
		}
	}

	void shake() {
		float val = Mathf.Sin(Time.time * speed);
		transform.position = new Vector3(
			originalPosition.x + Random.Range(0, val) * amount,
			transform.position.y,
			transform.position.z
		);
	}

	IEnumerator move() {
		yield return new WaitForSeconds(timeShaking);
		state = SliceState.Move;
		// Find place to go to on the other side of the player
		// Move there
		// Instantiate Lightning
		// Lightning lightning = Instantiate(lightningPrefab, transform.position, Quaternion.identity);
		// lightning.setDestination();
	}
}
