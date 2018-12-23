using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {
	bool playerTouches = false;
	bool canUse = true;

	[SerializeField] bool infiniteUse = true;
	[SerializeField] GameObject[] activatorsStates;

	void toggle () {
		foreach (var activator in activatorsStates) {
			activator.SetActive(!activator.activeSelf);
		}
	}

	void OnTriggerEnter2D() {
		if (canUse) {
			playerTouches = true;
		}
	}

	void OnTriggerExit2D() {
		if (canUse) {
			playerTouches = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (canUse && playerTouches && Input.GetButtonDown("Submit")) {
			if (!infiniteUse) {
				canUse = false;
			}
			toggle();
		}
	}
}
