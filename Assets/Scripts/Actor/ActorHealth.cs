using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorHealth : Observable {
	[SerializeField] int maxHealthInitial = 100;
	[SerializeField] float invincibilityDuration = 0f;
	[SerializeField] bool active = true;

	int maxHealth;
	int health;
	float timeSinceLastHit = 0f;

	HitResponse hitResponse;

	void Awake() {
		hitResponse = GetComponent<HitResponse>();
		maxHealth = maxHealthInitial;
		restore();
	}

	public void setActive(bool a) {
		active = a;
	}

	public void setHealth(int newHealth) {
		health = newHealth;
		notify();
	}

	public void restore() {
		health = maxHealth;
	}

	public void hit(DamageDealer damageDealer) {
		health -= damageDealer.getDamages();
		if (health < 0) {
			health = 0;
		}
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
		return active && Time.realtimeSinceStartup - timeSinceLastHit >= invincibilityDuration;
	}
}
