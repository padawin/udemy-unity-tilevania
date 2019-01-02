using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPlatform : MonoBehaviour {
	[SerializeField] float speed = 5f;
	[SerializeField] float minX;
	[SerializeField] float maxX;
	Collision2D myCollider = null;

	void OnCollisionEnter2D(Collision2D collider) {
		myCollider = collider;
		Debug.Log(myCollider);
	}

	void OnCollisionExit2D(Collision2D collider) {
		myCollider = null;
	}

	// Update is called once per frame
	void Update () {
		float newPosX = transform.position.x + Time.deltaTime * speed;
		if (myCollider != null) {
			myCollider.transform.position = new Vector2(
				myCollider.transform.position.x + Time.deltaTime * speed,
				myCollider.transform.position.y
			);
		}

		if (newPosX < minX) {
			newPosX = minX;
			speed *= -1;
		}

		if (newPosX > maxX) {
			newPosX = maxX;
			speed *= -1;
		}

		transform.position = new Vector2(
			newPosX,
			transform.position.y
		);
	}
}
