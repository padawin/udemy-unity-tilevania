using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogCollider : MonoBehaviour {
	int nbCollisions = 0;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.name != "Player") {
			return;
		}
		nbCollisions++;
		if (nbCollisions > 0) {
			gameObject.GetComponent<Renderer>().enabled = false;
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.name != "Player") {
			return;
		}
		nbCollisions--;
		if (nbCollisions <= 0) {
			gameObject.GetComponent<Renderer>().enabled = true;
		}
	}
}
