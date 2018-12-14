using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorTrigger : MonoBehaviour {
	[SerializeField] GameObject doorClosed;
	[SerializeField] GameObject doorOpened;

	void OnTriggerEnter2D() {
		doorClosed.SetActive(false);
		doorOpened.SetActive(true);
	}
}
