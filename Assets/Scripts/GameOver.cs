using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {
	SceneLoader sceneLoader;

	// Use this for initialization
	void Start () {
		sceneLoader = FindObjectOfType<SceneLoader>();
	}

	public void retry() {
		sceneLoader.loadLastLoadedScene();
	}
}
