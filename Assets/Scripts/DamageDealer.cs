using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {
	[SerializeField] int damages = 10;

	public int getDamages() {
		return damages;
	}
}
