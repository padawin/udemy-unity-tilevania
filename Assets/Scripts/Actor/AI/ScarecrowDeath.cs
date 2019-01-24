using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowDeath : MonoBehaviour {
	ActorHealth enemyHealth;
	bool isDead = false;
	GameObject nextLevel;

	// Use this for initialization
	void Start () {
		enemyHealth = GetComponent<ActorHealth>();
		nextLevel = FindObjectOfType<NextLevel>().gameObject;
		nextLevel.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		if (!isDead && enemyHealth.getHealth() <= 0) {
			isDead = true;
			nextLevel.SetActive(true);
			Destroy(gameObject, 3f);
		}
	}
}
