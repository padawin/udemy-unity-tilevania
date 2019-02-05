using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossDoor : Observer {
	[SerializeField] int countBeforeToggle = 0;

	public override void notify() {
		countBeforeToggle--;
		if (countBeforeToggle == 0) {
			gameObject.SetActive(!gameObject.activeSelf);
		}
	}
}
