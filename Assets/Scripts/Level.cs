using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
	[SerializeField] Player player;

	void Start () {
		GameSession gameSession = FindObjectOfType<GameSession>();
		SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
		gameSession.load();
		gameSession.saveCurrentLevelIndex(sceneLoader.getSceneIndex());
		// delete objects marked as to be deleted
		Vector2? playerposition = gameSession.getPlayerPosition();
		if (playerposition != null) {
			player.gameObject.transform.position = (Vector3) playerposition;
		}
	}
}
