using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Intro : MonoBehaviour {
	[SerializeField] float speed;
	[SerializeField] CinemachineVirtualCamera myCamera;
	[SerializeField] Vector2 playerDestination;
	[SerializeField] Actor player;

	bool headingTowardsPlayer = true;
	bool lockedOnPlayer = false;
	PlayerMovement playerMovement;

	void Start () {
		myCamera.Follow = transform;
		playerMovement = player.GetComponent<PlayerMovement>();
		player.block();
	}

	// Update is called once per frame
	void Update () {
		Vector2 direction = new Vector2(
			player.transform.position.x - transform.position.x,
			0f
		);
		if (headingTowardsPlayer) {
			moveTowards(direction);
			if (direction.magnitude < speed) {
				headingTowardsPlayer = false;
			}
		}
		else if (!lockedOnPlayer) {
			lockOnPlayer();
			makePlayerMove();
		}
		else if (playerArrivedToDestination()) {
			player.unblock();
			playerMovement.move(0f);
			Destroy(gameObject);
		}
	}

	void moveTowards(Vector2 direction) {
		direction.Normalize();
		direction *= speed;
		transform.position = new Vector2(
			transform.position.x + direction.x,
			transform.position.y + direction.y
		);
	}

	void lockOnPlayer() {
		myCamera.Follow = player.transform;
		lockedOnPlayer = true;
	}

	void makePlayerMove() {
		playerMovement.move(0.5f);
	}

	bool playerArrivedToDestination() {
		return player.transform.position.x >= playerDestination.x;
	}
}
