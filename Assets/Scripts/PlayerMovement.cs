using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float speed = 10f;

	Rigidbody2D rb;
	Animator myAnimator;

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
	}

    float getDelta() {
        return Input.GetAxis("Horizontal") * Time.deltaTime * speed;
    }

    void move() {
		float delta = getDelta();
		rb.velocity = new Vector2(getDelta(), rb.velocity.y);
		myAnimator.SetBool("Running", delta != 0);
    }

	void updateOrientation() {
		float direction = Mathf.Sign(rb.velocity.x);
		if (rb.velocity.x == 0 || direction == transform.localScale.x) {
			return;
		}
		transform.localScale = new Vector3(
			Mathf.Sign(rb.velocity.x),
			transform.localScale.y,
			transform.localScale.z
		);
    }

    // Update is called once per frame
    void Update() {
        move();
        updateOrientation();
    }
}
