using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Observer {
	public override void notify() {
		GetComponent<ActorHealth>().setActive(true);
	}
}
