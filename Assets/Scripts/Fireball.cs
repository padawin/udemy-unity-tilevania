using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {
	[SerializeField] Vector2 velocity;
	[SerializeField] GameObject explosion;

	Rigidbody2D rb;
	float direction = 1f;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	void Update () {
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

	public void setDirection(float newDirection) {
		direction = Mathf.Sign(newDirection);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Instantiate(explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
