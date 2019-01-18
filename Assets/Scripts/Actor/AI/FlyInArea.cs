using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyInArea : MonoBehaviour {
	[SerializeField] int defaultDirection;
	[SerializeField] Vector2 defaultDestination;
	[SerializeField] bool targetPlayer = false;
	[SerializeField] float speed;
	[SerializeField] float minX;
	[SerializeField] float maxX;
	[SerializeField] float minY;
	[SerializeField] float maxY;

	bool destinationEverSet = false;
	Vector2 destination;
	Vector2 lastKnownPosition;

	Rigidbody2D rb;
	Transform player;

	public void setDestination(Vector2 newDestination) {
		destination = new Vector2(newDestination.x, newDestination.y);
		destinationEverSet = true;
	}

	public void setSpeed(float speed) {
		this.speed = speed;
	}

	public void setArea(float minX, float maxX, float minY, float maxY) {
		this.minX = minX;
		this.maxX = maxX;
		this.minY = minY;
		this.maxY = maxY;
	}

	void Start() {
		rb = GetComponent<Rigidbody2D>();
		player = FindObjectOfType<Player>().gameObject.GetComponent<Transform>();
		if (!destinationEverSet) {
			findNextDestination();
		}
		updateLastKnownPosition();
	}

	void findNextDestination() {
		float x, y;
		if (targetPlayer) {
			x = player.position.x;
			y = player.position.y;
		}
		else {
			x = Random.Range(minX, maxX);
			y = Random.Range(minY, maxY);
		}
		destination = new Vector2(x, y);
	}

	void updateLastKnownPosition() {
		lastKnownPosition = new Vector2(transform.position.x, transform.position.y);
	}

	void Update () {
		if (reachedDestination()) {
			findNextDestination();
		}
		headTowardsDestination();
		updateLastKnownPosition();
	}

	bool reachedDestination() {
		return Mathf.Sign(destination.x - lastKnownPosition.x) != Mathf.Sign(destination.x - transform.position.x);
	}

	void headTowardsDestination() {
		Vector2 direction = new Vector2(
			destination.x - transform.position.x,
			destination.y - transform.position.y
		);
		rb.velocity = direction.normalized * speed;
		transform.localScale = new Vector2(
			Mathf.Sign(defaultDirection) * Mathf.Sign(direction.x),
			transform.localScale.y
		);
	}
}
