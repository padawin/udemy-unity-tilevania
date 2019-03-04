using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {
	bool playerTouches = false;
	bool canUse = true;

	[SerializeField] bool infiniteUse = true;
	[SerializeField] GameObject[] activatorsStates;
	[SerializeField] ActivatorObserver[] observers;

	void Start() {
		foreach (var observer in observers) {
			observer.register();
		}
	}

	public void toggle () {
		foreach (var activator in activatorsStates) {
			activator.SetActive(!activator.activeSelf);
		}
		foreach (var observer in observers) {
			observer.activate();
		}
		if (!infiniteUse) {
			canUse = false;
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
			GetComponent<Saveable>().save();
			toggle();
		}
	}
}
