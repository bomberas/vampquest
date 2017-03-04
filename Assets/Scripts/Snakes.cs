using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snakes : MonoBehaviour {

	public int powerAttack = 2;
	private PlayerControl player;
	private Animator anim;
	private bool isAttacking = false;

	void Start () {
		anim = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}
	
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other) {
		if ( other.tag == "Player" ) {
			Attack ();
		}	
	}

	void OnTriggerExit2D(Collider2D other) {
		if ( other.tag == "Player" ) {
			isAttacking = false;
			anim.SetBool ("isAttacking", isAttacking);
			anim.SetBool ("isQuiet", true);
		}
	}

	void Attack() {
		isAttacking = true;
		anim.SetBool ("isAttacking", isAttacking);
		player.Health -= powerAttack;
	}

	protected bool isInsideTheFrustrum() {
		Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
		return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
	}

}
