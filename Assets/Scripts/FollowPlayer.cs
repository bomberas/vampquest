using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public Vector3 offset;
	private Transform player;

	void Awake () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update () {
		transform.position = player.position + offset;
	}
}
