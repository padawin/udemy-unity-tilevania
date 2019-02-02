using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Observer {
	[SerializeField] Vector2 velocity;
	[SerializeField] GameObject explosion;

	Rigidbody2D rb;
	float direction = 1f;

	bool didHit = false;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	void Update () {
		move();
		collision();
	}

	void move() {
		rb.velocity = new Vector2(
			direction * velocity.x * Time.deltaTime,
			velocity.y * Time.deltaTime
		);
		transform.localScale = new Vector3(
			direction,
			transform.localScale.y,
			transform.localScale.z
		);
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

	public void setDirection(float newDirection) {
		direction = Mathf.Sign(newDirection);
	}

	void OnCollisionEnter2D(Collision2D collider) {
		explode();
	}

	override public void notify() {
		didHit = true;
	}
}
