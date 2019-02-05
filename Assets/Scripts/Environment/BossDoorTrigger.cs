using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorTrigger : Observable {
	bool triggered = false;

	void OnTriggerEnter2D() {
		if (triggered) {
			return;
		}
		notify();
		triggered = true;
		Destroy(gameObject);
	}
}
