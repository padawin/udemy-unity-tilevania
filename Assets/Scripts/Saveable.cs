using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SaveableType {ACTIVATE, DEACTIVATE, DESTROY, TOGGLE};

public class Saveable : MonoBehaviour {
	[SerializeField] SaveableType type;

	GameSession gameSession;
	string guid;

	public void save() {
		if (gameSession == null) {
			gameSession = FindObjectOfType<GameSession>();
		}
		gameSession.saveSaveable(guid);
	}

	void Start() {
		guid = GetComponent<Guid>().guid.ToString();
		gameSession = FindObjectOfType<GameSession>();
		if (gameSession.findSaveable(guid)) {
			switch (type) {
				case SaveableType.ACTIVATE:
					gameObject.SetActive(true);
					break;
				case SaveableType.DEACTIVATE:
					gameObject.SetActive(false);
					break;
				case SaveableType.DESTROY:
					Destroy(gameObject);
					break;
				case SaveableType.TOGGLE:
					GetComponent<Activator>().toggle();
					break;
			}
		}
	}
}
