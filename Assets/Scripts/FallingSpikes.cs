using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikes : MonoBehaviour {
	Rigidbody2D rb;
	float timeBeforeDestroy = 0.5f;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		rb.bodyType = RigidbodyType2D.Dynamic;
	}

	void OnCollisionEnter2D(Collision2D collider) {
		if (collider.gameObject.name == "Player") {
			Debug.Log("Damage");
		}
		Destroy(gameObject, timeBeforeDestroy);
	}
}
