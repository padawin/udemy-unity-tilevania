using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EndGameOutcome {Win, Lose};

public class ToEndGame : Observer {
	[SerializeField] EndGameOutcome outcome;
	SceneLoader sceneLoader;
	GameSession gameSession;

	void Start () {
		sceneLoader = FindObjectOfType<SceneLoader>();
		gameSession = FindObjectOfType<GameSession>();
	}

	public override void notify() {
		gameSession.setGameEnd(outcome);
		sceneLoader.loadSceneFromName("EndGame");
	}
}
