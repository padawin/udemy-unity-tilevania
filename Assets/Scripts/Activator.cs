using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {
	bool canActivate = false;

	[SerializeField] GameObject[] activatorsStates;

	void toggle () {
		foreach (var activator in activatorsStates) {
			activator.SetActive(!activator.activeSelf);
		}
	}

	void OnTriggerEnter2D() {
		canActivate = true;
	}

	void OnTriggerExit2D() {
		canActivate = false;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Submit")) {
			if (canActivate) {
				toggle();
			}
		}
	}
}
