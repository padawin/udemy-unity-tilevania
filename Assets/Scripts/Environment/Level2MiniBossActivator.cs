using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2MiniBossActivator : Observable {
	void OnTriggerEnter2D() {
		notify();
		Destroy(gameObject);
	}
}
