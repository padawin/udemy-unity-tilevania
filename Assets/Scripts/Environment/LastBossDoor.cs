using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBossDoor : ActivatorObserver {
	int nbSwitches = 0;
	int nbActiveSwitches = 0;
	[SerializeField] GameObject openState;
	[SerializeField] GameObject closedState;

	override public void register() {
		nbSwitches++;
	}

	override public void activate() {
		nbActiveSwitches++;
		if (nbActiveSwitches == nbSwitches) {
			openState.SetActive(true);
			closedState.SetActive(false);
		}
	}
}
