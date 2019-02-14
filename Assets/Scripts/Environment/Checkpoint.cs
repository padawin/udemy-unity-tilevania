using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
	[SerializeField] Player player;
	[SerializeField] SaveNotification notification;

	void OnTriggerEnter2D() {
		save();
		notification.show();
		Destroy(gameObject);
	}

	public void save() {
		GameSession gameSession = FindObjectOfType<GameSession>();
		gameSession.save(player.transform.position);
	}
}
