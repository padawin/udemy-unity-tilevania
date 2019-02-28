using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLife : Observer {
	public override void notify() {
		gameObject.SetActive(true);
	}
}
