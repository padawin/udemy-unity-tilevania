using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractText : MonoBehaviour {
	[SerializeField] string title;
	[SerializeField] string[] pages;
	[SerializeField] TextMeshProUGUI titleTextField;
	[SerializeField] TextMeshProUGUI textTextField;

	Canvas myCanvas;
	int currentPage = 0;

	void Start () {
		myCanvas = GetComponentInChildren<Canvas>();
		myCanvas.enabled = false;
		titleTextField.text = title;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		myCanvas.enabled = true;
		currentPage = 0;
		updateText();
	}

	void OnTriggerExit2D(Collider2D collider) {
		myCanvas.enabled = false;
	}

	void updateText() {
		textTextField.text = pages[currentPage];
	}

	void Update () {
		if (!myCanvas.enabled) {
			return;
		}
		if (Input.GetButtonDown("Submit")) {
			currentPage++;
			if (currentPage < pages.Length) {
				updateText();
			}
			else {
				myCanvas.enabled = false;
			}
		}
	}
}
