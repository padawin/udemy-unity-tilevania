using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
	SceneLoader sceneLoader;
	GameSession gameSession;

	// Use this for initialization
	void Start () {
		sceneLoader = FindObjectOfType<SceneLoader>();
		gameSession = FindObjectOfType<GameSession>();
	}

	public void loadGame() {
		gameSession.load();
		sceneLoader.loadScene(gameSession.getCurrentLevelIndex());
	}
}
