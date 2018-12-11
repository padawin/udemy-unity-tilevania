using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogCollider : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collider) {
		gameObject.GetComponent<Renderer>().enabled = false;
	}

	void OnTriggerExit2D(Collider2D collider) {
		gameObject.GetComponent<Renderer>().enabled = true;
	}
}
