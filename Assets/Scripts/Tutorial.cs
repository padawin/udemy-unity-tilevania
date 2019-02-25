using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
	void Start () {
		GameSession gameSession = FindObjectOfType<GameSession>();
		gameSession.clearSave();
		Destroy(gameObject);
	}
}
