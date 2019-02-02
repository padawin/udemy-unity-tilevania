using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observable : MonoBehaviour {
	[SerializeField] Observer[] observers;

	protected void notify() {
		foreach (Observer observer in observers) {
			observer.notify();
		}
	}
}
