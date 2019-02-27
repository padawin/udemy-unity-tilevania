using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : Observable {
	void OnTriggerEnter2D() {
		notify();
		Destroy(gameObject);
	}
}
