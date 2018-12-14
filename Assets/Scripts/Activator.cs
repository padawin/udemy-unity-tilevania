using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {
	bool canActivate = false;

	[SerializeField] GameObject activatorStateA;
	[SerializeField] GameObject activatorStateB;
	[SerializeField] GameObject objectStateA;
	[SerializeField] GameObject objectStateB;

	void toggle () {
		activatorStateA.SetActive(!activatorStateA.activeSelf);
		activatorStateB.SetActive(!activatorStateB.activeSelf);
		objectStateA.SetActive(!objectStateA.activeSelf);
		objectStateB.SetActive(!objectStateB.activeSelf);
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
