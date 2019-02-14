using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {
	[SerializeField] string bonusName;
	[SerializeField] Notification notification;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.name == "Player") {
			grant();
			notification.show(bonusName.ToUpper() + " BONUS GRANTED");
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
