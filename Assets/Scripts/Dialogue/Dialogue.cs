using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : Observer {
	[SerializeField] InteractText[] texts;
	[SerializeField] Actor player;
	PlayerMovement playerMovement;

	int currentText = 0;

	void Start() {
		playerMovement = player.GetComponent<PlayerMovement>();
		foreach (InteractText text in texts) {
			text.hide();
		}
	}

	override public void notify() {
		if (currentText < texts.Length) {
			if (currentText == 0) {
				playerMovement.setDelta(0f);
				player.block();
			}
			texts[currentText].display();
			currentText++;
		}
		else {
			player.unblock();
		}
	}
}
