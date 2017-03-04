using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public bool facingRight = true;
	public float moveForce = 365f;			
	public float maxSpeed = 5f;				
	public float jumpForce = 1000f;			
	private int health = 100;

	private bool jump = false;
	private bool m_attackPressed = false;
	private bool isAttacking = false;
	private bool grounded = false;			
	private Animator anim;					


	void Awake(){
		anim = GetComponent<Animator>();
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
		anim.SetFloat("Speed", Mathf.Abs(h));

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

	IEnumerator Attack(){
		isAttacking = true;
		anim.SetBool ("attack", true);
		anim.speed = .5f;
		yield return new WaitForSeconds (.5f);			
		anim.SetBool ("attack", false);
		isAttacking = false;
		yield return null;
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