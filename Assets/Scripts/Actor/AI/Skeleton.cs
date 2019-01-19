using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour {
	EnemyBackForthMovement backForth;
	JumpTowardsPlayer jumpTowards;
	[SerializeField] Collider2D playerDetector;

	bool seesPlayer = false;

	void Start () {
		backForth = GetComponent<EnemyBackForthMovement>();
		jumpTowards = GetComponent<JumpTowardsPlayer>();
		backForth.enabled = true;
		jumpTowards.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		bool nowSeesPlayer = playerDetector.IsTouchingLayers(LayerMask.GetMask("Player"));
		if (nowSeesPlayer != seesPlayer) {
			seesPlayer = nowSeesPlayer;
			toggleBehaviours();
		}
	}

	void toggleBehaviours() {
		backForth.enabled = !seesPlayer;
		jumpTowards.enabled = seesPlayer;
	}
}
