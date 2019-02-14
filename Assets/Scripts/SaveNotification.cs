using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNotification : MonoBehaviour {
	[SerializeField] float timeVisible;
	[SerializeField] Canvas myCanvas;

	void Start () {
		StartCoroutine(hide(0));
	}

	public void show() {
		gameObject.SetActive(true);
		StartCoroutine(hide(timeVisible));
	}

	IEnumerator hide(float time) {
		yield return new WaitForSeconds(time);
		gameObject.SetActive(false);
	}
}
