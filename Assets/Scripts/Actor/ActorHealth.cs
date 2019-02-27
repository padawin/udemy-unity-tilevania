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
		initMaxHealth();
		restore();
	}

	void initMaxHealth() {
		if (maxHealth == 0) {
			maxHealth = maxHealthInitial;
		}
	}

	public void setMaxHealth(int extra) {
		maxHealth = maxHealthInitial + extra;
	}

	public int getMaxHealthExtra() {
		return maxHealth - maxHealthInitial;
	}

	public void restore() {
		health = maxHealth;
	}

	public void increaseHealth(int val) {
		health += val;
	}

	public void increaseMaxHealth(int val) {
		maxHealth += val;
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
