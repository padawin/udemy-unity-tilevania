using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitResponse : MonoBehaviour {
	abstract public void run(GameObject source);
	abstract public void end();
}
