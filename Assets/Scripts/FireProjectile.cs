using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : Observer {
	[SerializeField] GameObject explosion;

	Rigidbody2D rb;

	bool didHit = false;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	void Update() {
		float direction = Mathf.Sign(rb.velocity.y);
		float angle;
		if (direction == 1) {
			angle = 90f;
		}
		else {
			angle = -90f;
		}
		transform.rotation = Quaternion.Euler(
			0, 0,
			angle
		);
		collision();
	}

	void OnTriggerEnter2D() {
		explode();
	}

	void collision() {
		if (didHit) {
			explode();
		}
	}

	void explode() {
		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D collider) {
		explode();
	}

	override public void notify() {
		didHit = true;
	}
}
