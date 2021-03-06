using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {
	HashSet<string> playerBonuses;
	HashSet<string> saveableObjects;

	EndGameOutcome outcome;

	void Awake () {
		playerBonuses = new HashSet<string>();
		saveableObjects = new HashSet<string>();
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

	public void save(Vector2 playerPosition, int playerHealth) {
		saveBonuses();
		saveObjects();
		savePlayerPosition(playerPosition);
		savePlayerHealth(playerHealth);
		PlayerPrefs.Save();
	}

	public bool hasSavedLevel() {
		return PlayerPrefs.HasKey("Level");
	}

	public void saveCurrentLevelIndex(int index) {
		PlayerPrefs.SetInt("Level", index);
	}

	public void load() {
		disableBonusesFromScene();
		saveableObjects = new HashSet<string>();
		if (PlayerPrefs.HasKey("PlayerBonuses")) {
			string b = PlayerPrefs.GetString("PlayerBonuses");
			string[] bonuses = b.Split(',');
			playerBonuses = new HashSet<string>(bonuses);
		}
		if (PlayerPrefs.HasKey("SaveableObjects")) {
			string[] objects = PlayerPrefs.GetString("SaveableObjects").Split(',');
			saveableObjects = new HashSet<string>(objects);
		}
	}

	public void loadPlayer(Player player) {
		if (PlayerPrefs.HasKey("PlayerHealth")) {
			player.GetComponent<ActorHealth>().setHealth(
				PlayerPrefs.GetInt("PlayerHealth")
			);
		}
	}

	public int getCurrentLevelIndex() {
		return PlayerPrefs.GetInt("Level");
	}

	private void saveBonuses() {
		string[] bonuses = new string[playerBonuses.Count];
		playerBonuses.CopyTo(bonuses);
		PlayerPrefs.SetString(
			"PlayerBonuses",
			string.Join(",", bonuses)
		);
	}

	private void saveObjects() {
		string[] objects = new string[saveableObjects.Count];
		saveableObjects.CopyTo(objects);
		PlayerPrefs.SetString(
			"SaveableObjects",
			string.Join(",", objects)
		);
	}

	public bool findSaveable(string objectID) {
		bool isSaved = saveableObjects.Contains(objectID);
		return isSaved;
	}

	public void saveSaveable(string objectID) {
		saveableObjects.Add(objectID);
	}

	private void savePlayerPosition(Vector2 position) {
		PlayerPrefs.SetFloat("PlayerX", position.x);
		PlayerPrefs.SetFloat("PlayerY", position.y);
	}

	private void savePlayerHealth(int health) {
		PlayerPrefs.SetInt("PlayerHealth", health);
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
		PlayerPrefs.Save();
	}

	public void clearLevel() {
		PlayerPrefs.DeleteKey("PlayerX");
		PlayerPrefs.DeleteKey("PlayerY");
		PlayerPrefs.DeleteKey("SaveableObjects");
		PlayerPrefs.Save();
	}

	public void setGameEnd(EndGameOutcome o) {
		outcome = o;
	}

	public EndGameOutcome getGameEnd() {
		return outcome;
	}
}
