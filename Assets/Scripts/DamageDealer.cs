using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {
	[SerializeField] Collider2D hitbox;
	[SerializeField] int damages = 10;

	public int getDamages() {
		return damages;
	}

	public Collider2D getHitbox() {
		return hitbox;
	}
}
