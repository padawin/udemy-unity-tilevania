using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
	[SerializeField] Player player;
	[SerializeField] Notification notification;

	void OnTriggerEnter2D() {
		save();
		Destroy(gameObject);
	}

	public void save() {
		GameSession gameSession = FindObjectOfType<GameSession>();
		gameSession.save(
			player.transform.position, player.GetComponent<ActorHealth>().getHealth()
		);
		notification.show("GAME SAVED");
	}
}
