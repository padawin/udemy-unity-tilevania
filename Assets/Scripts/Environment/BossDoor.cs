using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : Observer {
	bool isActive;

	void Start() {
		isActive = gameObject.activeSelf;
	}

	public override void notify() {
		gameObject.SetActive(!isActive);
	}
}
