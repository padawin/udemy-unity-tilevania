using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {
	ActorHealth enemyHealth;
	bool isDead = false;

	// Use this for initialization
	void Start () {
		enemyHealth = GetComponent<ActorHealth>();
		if (enemyHealth == null) {
			enemyHealth = GetComponentInChildren<ActorHealth>();
		}
	}

	// Update is called once per frame
	void Update () {
		if (!isDead && enemyHealth.getHealth() <= 0) {
			isDead = true;
			DamageDealer dd = GetComponent<DamageDealer>();
			if (dd == null) {
				dd = GetComponentInChildren<DamageDealer>();
			}
			dd.deactivate();
			Destroy(gameObject);
		}
	}
}
