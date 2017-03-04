using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float moveSpeed = 2f;
	public int HP = 2;
	public int powerAttack = 10;
	public GameObject hundredPointsUI;
	public float deathSpinMin = -100f;
	public float deathSpinMax = 100f;

	//private Vampire vampire;
	private Animator anim;
	private Transform frontCheck;
	private bool dead = false;
	private bool isStarting = true;
	private bool isAppearing = true;
	private bool isAttacking = false;
	//private Score score;

	void Awake() {
		anim = GetComponent<Animator>();
		frontCheck = transform.gameObject.transform;
		//player = GameObject.FindGameObjectWithTag("Player").GetComponent<Vampire>;
		//score = GameObject.Find("Score").GetComponent<Score>();
	}

	void FixedUpdate () {
		if ( isStarting && isAppearing ) {
			StartCoroutine (Appear ());
			isStarting = false;
		}

		if ( !isAppearing && !dead ) {
			Collider2D[] frontHits = Physics2D.OverlapPointAll (frontCheck.position, 1);

			foreach ( Collider2D c in frontHits ) {
				if ( c.tag == "Obstacle" ) {
					Flip ();
					break;
				}
			}

			if ( HP <= 0 && !dead ) {
				Death ();
			} else {
				Walk ();
			}
		}
	}

	void Walk() {
		if ( !dead && !isAttacking ) {
			ResetAnimations ();
			anim.SetBool ("isMoving", true);
			GetComponent<Rigidbody2D> ().velocity = new Vector2 ( -transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);	
		}
	}

	void Death() {
		dead = true;
		anim.SetBool ("isDead", dead);

		// Increase the score by 100 points
		//		score.score += 100;
		StartCoroutine(Die());

		Vector3 scorePos = transform.position;
		scorePos.y += 1.5f;

		// Instantiate the 100 points prefab at this point.
		Instantiate(hundredPointsUI, scorePos, Quaternion.identity);
	}

	void OnTriggerStay2D(Collider2D other) {
		if ( other.tag == "Player" ) {
			Attack ();
		}	
	}

	void OntriggerExit2D(Collider2D other) {
		if ( other.tag == "Player" ) {
			isAttacking = false;
			anim.SetBool ("isAttacking", isAttacking);
		}
	}

	void Attack() {
		if ( !dead ) {
			ResetAnimations ();
			isAttacking = true;
			anim.SetBool ("isAttacking", isAttacking);
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			//vampire.Health += powerAttack;
		}
	}

	void Flip() {
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}

	void ResetAnimations() {
		anim.SetBool ("isDead", false);
		anim.SetBool ("isQuiet", false);
		anim.SetBool ("isMoving", false);
		anim.SetBool ("isAttacking", false);
	}

	IEnumerator Appear() {
		yield return new WaitForSeconds(2.0f);
		isAppearing = false;
	}

	IEnumerator Die() {
		yield return new WaitForSeconds(3.0f);
		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		gameObject.SetActive (false);
	}

	public void Hurt() {
		HP--;
	}

}
