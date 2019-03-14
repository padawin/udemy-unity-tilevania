using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEndText : MonoBehaviour {
	[TextArea]
	[SerializeField] string textWin;
	[TextArea]
	[SerializeField] string textLose;
	TextMeshProUGUI textField;
	GameSession gameSession;

	void Start () {
		textField = GetComponent<TextMeshProUGUI>();
		gameSession = FindObjectOfType<GameSession>();
		if (gameSession.getGameEnd() == EndGameOutcome.Win) {
			textField.text = textWin.ToUpper();
		}
		else {
			textField.text = textLose.ToUpper();
		}
	}
}
