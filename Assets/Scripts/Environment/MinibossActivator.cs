using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinibossActivator : Observer {
	[SerializeField] GameObject[] lava;
	[SerializeField] float timeBetweenEnableBlocks;

	bool lavaEnabled = false;

	void Start() {
		for (int i = 0; i < lava.Length; ++i) {
			lava[i].SetActive(false);
		}
	}

	void OnTriggerEnter2D() {
		if (lavaEnabled) {
			return;
		}
		StartCoroutine(enableLava());
		lavaEnabled = true;
	}

	IEnumerator enableLava() {
		for (int i = 0; i < lava.Length; i += 2) {
			if (i < lava.Length) {
				lava[i].SetActive(true);
			}
			if (i + 1 < lava.Length) {
				lava[i + 1].SetActive(true);
			}
			if (i + 2 < lava.Length) {
				yield return new WaitForSeconds(timeBetweenEnableBlocks);
			}
		}
		enableLavaAnimator();
	}

	void enableLavaAnimator() {
		for (int i = 0; i < lava.Length; ++i) {
			lava[i].GetComponent<Animator>().enabled = true;
		}
	}

	override public void notify() {
		StartCoroutine(disableLava());
	}

	IEnumerator disableLava() {
		for (int i = 0; i < lava.Length; i += 2) {
			if (i < lava.Length) {
				lava[i].SetActive(false);
			}
			if (i + 1 < lava.Length) {
				lava[i + 1].SetActive(false);
			}
			if (i + 2 < lava.Length) {
				yield return new WaitForSeconds(timeBetweenEnableBlocks);
			}
		}
	}
}
