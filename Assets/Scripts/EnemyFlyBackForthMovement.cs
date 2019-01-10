using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyBackForthMovement : EnemyBackForthMovement {
	[SerializeField] float ySpeed = 0f;
	[SerializeField] float minY;
	[SerializeField] float maxY;

	new void Start() {
		base.Start();
		rb.velocity = new Vector2(rb.velocity.x, ySpeed);
	}

	new void Update () {
		updateAltitude();
		base.Update();
	}

	void updateAltitude() {
		if (transform.position.y < minY || maxY < transform.position.y) {
			ySpeed *= -1;
			rb.velocity = new Vector2(rb.velocity.x, ySpeed);
			transform.position = new Vector2(
				transform.position.x,
				Mathf.Clamp(transform.position.y, minY, maxY)
			);
		}
	}
}
