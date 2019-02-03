using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour {
	[SerializeField] InteractText text;

	void OnTriggerEnter2D() {
		text.display();
	}

	void OnTriggerExit2D() {
		text.hide();
	}
}
