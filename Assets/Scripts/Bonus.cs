using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {
	[SerializeField] string bonusName;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.name == "Player") {
			grant();
			Destroy(gameObject);
		}
	}

	void grant() {
		FindObjectOfType<GameSession>().addPlayerBonus(bonusName);
	}

	public string getName() {
		return bonusName;
	}
}
