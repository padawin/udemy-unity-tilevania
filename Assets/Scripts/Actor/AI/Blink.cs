using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour {
	[SerializeField] float minTimeVisible;
	[SerializeField] float maxTimeVisible;
	[SerializeField] float minTimeInvisible;
	[SerializeField] float maxTimeInvisible;
	[SerializeField] float minX;
	[SerializeField] float maxX;
	[SerializeField] float minY;
	[SerializeField] float maxY;

	Vector2 destination;
	bool needsToAppear = false;
	bool needsToDisappear = false;

	Animator myAnimator;
	CapsuleCollider2D myCollider;
	SpriteRenderer mySpriteRenderer;

	// Use this for initialization
	void Start () {
		myAnimator = GetComponent<Animator>();
		myCollider = GetComponent<CapsuleCollider2D>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		StartCoroutine(disappear());
	}

	IEnumerator disappear() {
		yield return new WaitForSeconds(Random.Range(minTimeVisible, maxTimeVisible));
		myAnimator.SetTrigger("Disappear");
	}

	void setInvisible() {
		needsToAppear = true;
		myCollider.enabled = false;
		mySpriteRenderer.enabled = false;
	}

	void goToNextDestination() {
		float x = Random.Range(minX, maxX);
		float y = Random.Range(minY, maxY);
		transform.position = new Vector2(x, y);
	}

	void Update() {
		if (needsToAppear) {
			needsToAppear = false;
			goToNextDestination();
			StartCoroutine(appear());
		}
		else if (needsToDisappear) {
			needsToDisappear = false;
			StartCoroutine(disappear());
		}
	}

	IEnumerator appear() {
		yield return new WaitForSeconds(Random.Range(minTimeInvisible, maxTimeInvisible));
		mySpriteRenderer.enabled = true;
		myAnimator.SetTrigger("Appear");
	}

	void setVisible() {
		needsToDisappear = true;
		myCollider.enabled = true;
	}
}
