using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Observer {
	public override void notify() {
		gameObject.SetActive(true);
	}
}
