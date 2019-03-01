using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : Observer {
	[SerializeField] ActorHealth actorHealth;

	void Start() {
		updateWidth();
	}

	public override void notify() {
		updateWidth();
	}

	void updateWidth() {
		float healthRatio = actorHealth.getHealth() / (float) actorHealth.getMaxHealth();
		transform.localScale = new Vector2(healthRatio, transform.localScale.y);
	}
}
