using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour {
	[SerializeField] int maxMana = 100;

	int mana;

	GameSession gameSession;

	void Start () {
		mana = maxMana;
		gameSession = FindObjectOfType<GameSession>();
	}

	public void consume(int amount) {
		mana -= amount;
		if (mana < 0) {
			mana = 0;
		}
	}
}
