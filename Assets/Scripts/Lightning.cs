using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LightningState {Grow, Stable, Shrink};
public class Lightning : MonoBehaviour {
	[SerializeField] float growthSpeed = 0.1f;
	[SerializeField] float timeBeforeShrink = 1f;

	Vector2 initialPosition;
	float direction;
	float maxLength;

	LightningState state = LightningState.Grow;

	BoxCollider2D myCollider;
	SpriteRenderer myRenderer;

	void Start() {
		myCollider = GetComponent<BoxCollider2D>();
		myRenderer = GetComponent<SpriteRenderer>();
		myCollider.size = new Vector2(0f, myCollider.size.y);
		myRenderer.size = new Vector2(0f, myRenderer.size.y);
	}

	public void setDestination(Vector2 destination) {
		initialPosition = transform.position;
		maxLength = Mathf.Abs(destination.x - transform.position.x);
		direction = Mathf.Sign(destination.x - transform.position.x);
	}

	// Update is called once per frame
	void Update () {
		if (state == LightningState.Grow) {
			grow();
		}
		else if (state == LightningState.Shrink) {
			shrink();
		}
	}

	void grow() {
		float growth = growthSpeed * Time.deltaTime;
		updateSize(growth);
		if (myCollider.size.x >= maxLength) {
			state = LightningState.Stable;
			StartCoroutine(startShrinking());
		}
	}

	IEnumerator startShrinking() {
		yield return new WaitForSeconds(timeBeforeShrink);
		state = LightningState.Shrink;
		if (direction == 1f) {
			initialPosition = new Vector2(initialPosition.x + myCollider.size.x, initialPosition.y);
		}
		else {
			initialPosition = transform.position;
		}
		direction *= -1;
	}

	void shrink() {
		float growth = -1 * growthSpeed * Time.deltaTime;
		if (-1 * growth < myCollider.size.x) {
			updateSize(growth);
		}
		else {
			Destroy(gameObject);
		}
	}

	void updateSize(float growth) {
		myCollider.size = new Vector2(myCollider.size.x + growth, myCollider.size.y);
		// Collider needs to have the offset updated as well as the size, as its
		// pivot is in the center
		myCollider.offset = new Vector2(myCollider.size.x / 2, myCollider.offset.y);
		myRenderer.size = new Vector2(myRenderer.size.x + growth, myRenderer.size.y);
		if (direction == -1) {
			transform.position = new Vector2(
				initialPosition.x - myCollider.size.x, transform.position.y
			);
		}

	}
}
