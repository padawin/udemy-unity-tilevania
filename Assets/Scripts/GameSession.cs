using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {
	HashSet<string> playerBonuses;

	void Awake () {
		playerBonuses = new HashSet<string>();
		SetUpSingleton();
	}

	private void SetUpSingleton() {
		if (FindObjectsOfType(GetType()).Length > 1) {
			Destroy(gameObject);
		}
		else {
			DontDestroyOnLoad(gameObject);
		}
	}

	public bool playerHasBonus(string bonus) {
		return playerBonuses.Contains(bonus);
	}

	public void addPlayerBonus(string bonus) {
		playerBonuses.Add(bonus);
	}
}
