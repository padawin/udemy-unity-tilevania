using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
	[SerializeField] Player player;

	void OnTriggerEnter2D() {
		save();
		Destroy(gameObject);
	}

	public void save() {
		Debug.Log("Check Point!");
		GameSession gameSession = FindObjectOfType<GameSession>();
		gameSession.save(player.transform.position);
	}
}
