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
			sceneLoader.loadNextScene();
		}
	}
}
