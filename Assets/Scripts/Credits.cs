using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Credits : MonoBehaviour {
	[SerializeField] float timeDisplayText = 1f;
	[SerializeField] TextMeshProUGUI[] texts;
	bool played = false;

	void Start () {
		foreach (TextMeshProUGUI text in texts) {
			text.enabled = false;
		}
	}

	void Update () {
	}

	IEnumerator play(int currentTextIndex = 0) {
		if (currentTextIndex == 0 && played) {
			yield return null;
		}
		else if (!played || currentTextIndex < texts.Length) {
			played = true;
			texts[currentTextIndex].enabled = true;
			yield return new WaitForSeconds(timeDisplayText);
			texts[currentTextIndex].enabled = false;
			StartCoroutine(play(currentTextIndex + 1));
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		StartCoroutine(play());
	}
}
