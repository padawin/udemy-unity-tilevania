using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorHealth : MonoBehaviour {
	[SerializeField] int maxHealthInitial = 100;
	int maxHealth;
	[SerializeField] int health;

	HitResponse hitResponse;

	void Start() {
		hitResponse = GetComponent<HitResponse>();
		if (maxHealth == 0) {
			maxHealth = maxHealthInitial;
		}
		restore();
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
	}

	void OnTriggerEnter2D(Collider2D collider) {
		DamageDealer damageDealer = collider.GetComponent<DamageDealer>();
		if (damageDealer == null || collider != damageDealer.getHitbox()) {
			return;
		}

		hit(damageDealer);
		if (hitResponse != null) {
			hitResponse.run(collider.gameObject);
		}
	}

	public int getHealth() {
		return health;
	}

	public int getMaxHealth() {
		return maxHealth;
	}
}
