using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour {
	[SerializeField] float speed = 5f;
	[SerializeField] float minY;
	[SerializeField] float maxY;
	Collider2D myCollider = null;

	void OnTriggerEnter2D(Collider2D collider) {
		myCollider = collider;
	}

	void OnTriggerExit2D(Collider2D collider) {
		myCollider = null;
	}

	void Update() {
		float newPosY = transform.position.y;
		if (myCollider != null) {
			if (transform.position.y < maxY) {
				newPosY = transform.position.y + Time.deltaTime * speed;
				myCollider.transform.position = new Vector2(
					myCollider.transform.position.x,
					myCollider.transform.position.y + Time.deltaTime * speed
				);
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
