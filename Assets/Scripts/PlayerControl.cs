using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public bool facingRight = true;
	public float moveForce = 365f;			
	public float maxSpeed = 5f;				
	public float jumpForce = 1000f;			
<<<<<<< HEAD
=======
	private int health = 100;
	private long score = 0;
>>>>>>> 797b254805c7a82236e8b26416c95801acd928e5

	private int health = 100000;
	private Vector3 healthScale;
	private bool jump = false;
	private bool m_attackPressed = false;
	private bool isAttacking = false;
	private bool grounded = false;			
	private Animator anim;					
	private SpriteRenderer healthBar;

	void Awake(){
		anim = GetComponent<Animator>();
		healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
		healthScale = healthBar.transform.localScale;
	}


	void Update()
	{
		m_attackPressed = Input.GetButtonDown("Fire1");

		if(Input.GetButtonDown("Jump") && grounded)
			jump = true;

		if (m_attackPressed && !isAttacking) {
			StartCoroutine (Attack());
		}
	}


	void FixedUpdate (){
		float h = Input.GetAxis("Horizontal");
		//anim.SetFloat("Speed", Mathf.Abs(h));

		if(h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

		if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
			GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if(h > 0 && !facingRight)
			Flip();
		else if(h < 0 && facingRight)
			Flip();

		if(jump){
			anim.SetBool("jump", true);
//			AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
		UpdateHealthBar ();
	}
	
	void Flip () {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Ground" ){ 
			grounded = true;
			anim.SetBool("jump", false);
		}
	}

	private void OnTriggerStay2D (Collider2D other){
		if (other.tag == "Enemy") {
			if (isAttacking)
				other.GetComponent<Enemy>().Hurt();
		}
	}

	public void CollectBonusItem(int score, int heal){
		score += score;

		if (health + heal > 100) {
			health = 100;
		} else {
			health += heal;
		}
	}

	IEnumerator Attack(){
		isAttacking = true;
		anim.SetBool ("attack", true);
		anim.speed = .5f;
		yield return new WaitForSeconds (.5f);			
		anim.SetBool ("attack", false);
		isAttacking = false;
		yield return null;
	}

	public void UpdateHealthBar () {
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.00001f, 1, 1);
		print (health);
	}

	public int Health {
		get {
			return health;
		}
		set {
			health = value;
		}
	}
}