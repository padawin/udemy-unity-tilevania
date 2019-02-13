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

	public void disableBonusesFromScene() {
		Bonus[] bonuses = FindObjectsOfType<Bonus>();
		foreach (Bonus bonus in bonuses) {
			playerBonuses.Remove(bonus.getName());
		}
	}

	public void save(Vector2 position) {
		saveBonuses();
		savePlayerPosition(position);
	}

	public void load() {
		disableBonusesFromScene();
		if (PlayerPrefs.HasKey("PlayerBonuses")) {
			string b = PlayerPrefs.GetString("PlayerBonuses");
			string[] bonuses = b.Split(',');
			playerBonuses = new HashSet<string>(bonuses);
		}
	}
	private void saveBonuses() {
		string[] bonuses = new string[playerBonuses.Count];
		playerBonuses.CopyTo(bonuses);
		PlayerPrefs.SetString(
			"PlayerBonuses",
			string.Join(",", bonuses)
		);
	}

	private void savePlayerPosition(Vector2 position) {
		PlayerPrefs.SetFloat("PlayerX", position.x);
		PlayerPrefs.SetFloat("PlayerY", position.y);
	}

	public Vector2? getPlayerPosition() {
		if (!PlayerPrefs.HasKey("PlayerX")) {
			return null;
		}

		float x = PlayerPrefs.GetFloat("PlayerX"),
			  y = PlayerPrefs.GetFloat("PlayerY");
		return new Vector2(x, y);
	}

	public void clearSave() {
		PlayerPrefs.DeleteAll();
	}

	public void clearLevel() {
		PlayerPrefs.DeleteKey("PlayerX");
		PlayerPrefs.DeleteKey("PlayerY");
	}
}
