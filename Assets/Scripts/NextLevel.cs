using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour {
	SceneLoader sceneLoader;

	void Start () {
		sceneLoader = FindObjectOfType<SceneLoader>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.name == "Player") {
			GameSession gameSession = FindObjectOfType<GameSession>();
			gameSession.save(
				collider.transform.position,
				collider.GetComponent<ActorHealth>().getHealth()
			);
			gameSession.clearLevel();
			sceneLoader.loadNextScene();
		}
	}
}
