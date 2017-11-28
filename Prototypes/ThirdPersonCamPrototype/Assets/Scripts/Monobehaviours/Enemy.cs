using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public GameObject player;
	NavMeshAgent agent;
	Vector3 enemyPosition;
	Vector3 playerPosition;
	Vector3 direction;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		DetectionDistance ();

		agent.SetDestination (player.transform.position);
	}

	void DetectionDistance() {
		enemyPosition = this.gameObject.transform.position;
		playerPosition = player.transform.position;
		direction = playerPosition - enemyPosition;

		Debug.DrawRay (transform.position, direction, Color.green);
	}
}
