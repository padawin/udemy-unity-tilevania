using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {
	[SerializeField] int damages = 10;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.name != "Player") {
			return;
		}
		ActorHealth health= collider.GetComponent<ActorHealth>();
		health.hit(this);
	}

	public int getDamages() {
		return damages;
	}
}
