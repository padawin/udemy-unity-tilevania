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
	Grid myGrid;
	int currentPage = 0;

	void Start () {
		myCanvas = GetComponentInChildren<Canvas>();
		myGrid = GetComponentInChildren<Grid>();
		myCanvas.enabled = false;
		myGrid.gameObject.SetActive(false);
		titleTextField.text = title.ToUpper();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		myCanvas.enabled = true;
		myGrid.gameObject.SetActive(true);
		currentPage = 0;
		updateText();
	}

	void OnTriggerExit2D(Collider2D collider) {
		myCanvas.enabled = false;
		myGrid.gameObject.SetActive(false);
	}

	void updateText() {
		textTextField.text = pages[currentPage].ToUpper();
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
				myGrid.gameObject.SetActive(false);
			}
		}
	}
}
