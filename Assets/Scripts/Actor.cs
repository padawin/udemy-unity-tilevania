using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {
	bool blocked = false;

	/* For external interaction, eg. when being hit */
	public void block() {
		blocked = true;
	}

	public void unblock() {
		blocked = false;
	}

	public bool isBlocked() {
		return blocked;
	}
}
