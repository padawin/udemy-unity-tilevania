using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthDisplay : Observer {
	[SerializeField] ActorHealth playerHealth;
	[SerializeField] float widthMaxHealth;

	void Start() {
		updateWidth();
	}

	public override void notify() {
		updateWidth();
	}

	void updateWidth() {
		float healthRatio = playerHealth.getHealth() / (float) playerHealth.getMaxHealth();
		transform.localScale = new Vector2(healthRatio, transform.localScale.y);
	}
}
