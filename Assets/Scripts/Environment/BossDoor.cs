using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : Observer {
	Rigidbody2D rb;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

	public override void notify() {
		rb.bodyType = RigidbodyType2D.Dynamic;
	}
}
