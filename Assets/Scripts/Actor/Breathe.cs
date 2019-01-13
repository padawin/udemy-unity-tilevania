using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breathe : MonoBehaviour {
	/**
	 * Animation hook
	 */
	void changeBreath(float size) {
		transform.localScale = new Vector3(
			transform.localScale.x, size, transform.localScale.z
		);
	}
}
