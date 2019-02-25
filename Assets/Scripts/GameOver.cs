using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {
	SceneLoader sceneLoader;
	GameSession gameSession;

	// Use this for initialization
	void Start () {
		sceneLoader = FindObjectOfType<SceneLoader>();
		gameSession = FindObjectOfType<GameSession>();
	}

	void Update() {
		if (Input.GetButtonDown("Submit")) {
			sceneLoader.loadScene(gameSession.getCurrentLevelIndex());
		}
	}
}
