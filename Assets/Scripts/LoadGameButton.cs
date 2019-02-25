using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameButton : MonoBehaviour {
	[SerializeField] GameSession gameSession;

	void Start () {
		if (!gameSession.hasSavedLevel()) {
			gameObject.SetActive(false);
		}
	}
}
