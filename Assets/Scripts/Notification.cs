using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notification : MonoBehaviour {
	[SerializeField] float timeVisible;
	[SerializeField] Canvas myCanvas;
	[SerializeField] TextMeshProUGUI notificationTextField;

	void Start () {
		StartCoroutine(hide(0));
	}

	public void show(string text) {
		notificationTextField.text = text;
		gameObject.SetActive(true);
		StartCoroutine(hide(timeVisible));
	}

	IEnumerator hide(float time) {
		yield return new WaitForSeconds(time);
		gameObject.SetActive(false);
	}
}
