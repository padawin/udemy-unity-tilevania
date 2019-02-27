using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorHealth : Observable {
	[SerializeField] int maxHealthInitial = 100;
	[SerializeField] float invincibilityDuration = 0f;

	int maxHealth;
	int health;
	float timeSinceLastHit = 0f;

	HitResponse hitResponse;

	void Start() {
		hitResponse = GetComponent<HitResponse>();
		maxHealth = maxHealthInitial;
		restore();
	}

	public void setHealth(int newHealth) {
		health = newHealth;
		notify();
	}

	void restore() {
		health = maxHealth;
	}

	public void hit(DamageDealer damageDealer) {
		health -= damageDealer.getDamages();
		timeSinceLastHit = Time.realtimeSinceStartup;
		if (hitResponse != null) {
			hitResponse.run(damageDealer.gameObject);
		}
		notify();
	}

	public int getHealth() {
		return health;
	}

	public int getMaxHealth() {
		return maxHealth;
	}

	public bool canBeHit() {
		return Time.realtimeSinceStartup - timeSinceLastHit >= invincibilityDuration;
	}
}
