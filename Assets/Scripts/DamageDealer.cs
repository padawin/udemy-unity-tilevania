using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {
	[SerializeField] int damages = 10;

	void OnTriggerEnter2D(Collider2D collider) {
		ActorHealth health= collider.GetComponent<ActorHealth>();
		if (health != null && health.canBeHit()) {
			health.hit(this);
		}
	}

	public int getDamages() {
		return damages;
	}
}
