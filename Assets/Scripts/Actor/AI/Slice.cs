using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SliceState {Idle, Shake, Move};

public class Slice : MonoBehaviour {
	[SerializeField] float defaultDirection = -1f;
	[SerializeField] float timeShaking = 0;
	[SerializeField] float minX = 0;
	[SerializeField] float maxX = 0;
	[SerializeField] Lightning lightningPrefab;
	[SerializeField] Player player;
	float speed = 0.1f; // How fast it shakes
	float amount = 0.3f; // How much it shakes

	SliceState state = SliceState.Idle;
	Vector3 originalPosition;
	Lightning lightning;
	float destinationX;

	EvilLord evilLord;

	void Start() {
		evilLord = GetComponent<EvilLord>();
	}

	public void slice() {
		originalPosition = transform.position;
		state = SliceState.Shake;
		setDestination();
		updateDirection();
		StartCoroutine(move());

	}

	void setDestination() {
		if (player.transform.position.x < transform.position.x) {
			destinationX = Random.Range(minX, player.transform.position.x);
		}
		else {
			destinationX = Random.Range(player.transform.position.x, maxX);
		}
	}

	void updateDirection() {
		float spriteDirection = Mathf.Sign(
			player.transform.position.x - transform.position.x
		);
		transform.localScale = new Vector2(
			Mathf.Abs(transform.localScale.x) * spriteDirection * defaultDirection,
			transform.localScale.y
		);
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
		// Instantiate Lightning
		lightning = Instantiate(
			lightningPrefab, transform.position, Quaternion.identity
		);
		transform.position = new Vector2(destinationX, transform.position.y);
		lightning.setDestination(transform.position);
	}
}
