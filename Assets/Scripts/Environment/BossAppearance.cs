using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAppearance : MonoBehaviour {
	[SerializeField] GameObject[] enemies;

	void OnTriggerEnter2D() {
		foreach (GameObject enemy in enemies) {
			enemy.SetActive(true);
		}
		Destroy(gameObject);
	}
}
