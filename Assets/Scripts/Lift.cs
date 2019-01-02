using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour {
	[SerializeField] float speed = 5f;
	[SerializeField] float minY;
	[SerializeField] float maxY;
	bool playerOnIt = false;

	void OnTriggerEnter2D(Collider2D collider) {
		playerOnIt = true;
	}

	void OnTriggerExit2D(Collider2D collider) {
		playerOnIt = false;
	}

	void Update() {
		float newPosY = transform.position.y;
		if (playerOnIt) {
			if (transform.position.y < maxY) {
				newPosY = transform.position.y + Time.deltaTime * speed;
			}
		}
		else if (transform.position.y > minY) {
			newPosY = transform.position.y - Time.deltaTime * speed;
		}

		transform.position = new Vector2(
			transform.position.x,
			newPosY
		);
	}
}
