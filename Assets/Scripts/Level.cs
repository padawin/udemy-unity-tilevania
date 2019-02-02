using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameSession gameSession = FindObjectOfType<GameSession>();
		gameSession.disableBonusesFromScene();
	}
}
