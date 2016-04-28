using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float maxSpeed = 10f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float jumpForce = 200f;
	public int HP =1;
	public float invincible_time  = 1f;
	public SpriteRenderer renderer;
	public int eatingCD = 0;

	private bool facingRight = true;
	private bool grounded = false;
	private Rigidbody2D rb2d;
	private float groundRadius = 0.2f;
	private int invincible = 1;
	private int direction = 1;
	private bool eating = false;
	private Animator animator;
	private bool isJumping = false;

	public AudioClip jumpSound;
	public AudioClip GetHpSound;

	void Start () 
	{
		animator = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();	
	}

	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		float move = Input.GetAxis ("Horizontal");
		Vector2 movement = new Vector2 (move * maxSpeed, rb2d.velocity.y);
		rb2d.velocity = movement;

		if (move > 0 && !facingRight) {
			direction = -1;
			Flip ();
		} 
		else if (move < 0 && facingRight) {
			direction = 1;
			Flip ();
		}
	}

	void Update () 
	{   

		if (grounded) {
			isJumping = false;
		}

		if (grounded && Input.GetKeyDown (KeyCode.Space)) 
		{   
			if (isJumping == false) {
				rb2d.AddForce (new Vector2 (0, jumpForce));
				animator.SetTrigger ("Jump");
				SoundManager.instance.PlaySingle (jumpSound);
				isJumping = true;
			}

		}
		if (Input.GetKeyDown (KeyCode.C)) 
		{	
			if (eating == false && eatingCD == 0) {
				EatingMode (true);
				Invoke ("CancelEatingMode", 3);

			}
		}
		if (eatingCD > 0)
			eatingCD--;
	}
	void loseHP()
	{
		if (HP > 0 && invincible != 0) {
			HP--;
			invincible = 1;
			EatingMode (false);
		}
	}

	void EatingMode(bool status)
	{
		if (status) {
			animator.SetTrigger ("Eating");
			eatingCD = 300;
			renderer.color = Color.red;
			eating = true;

		} else {
			renderer.color = Color.white;
			eating = false;
		}
	}

	void CancelEatingMode(){
		renderer.color = Color.white;
		eating = false;
	}


	void Flip () 
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy") {
			if (eating) {
				other.gameObject.SetActive (false);
				EatingMode (false);
			} else {
				rb2d.AddForce (new Vector2 (direction * 10000f, 300f));
				isJumping = true;
				loseHP ();
			}
		}

		if (other.tag == "Trap") {
			rb2d.AddForce (new Vector2 (0f , 1000f));
			loseHP ();
		}

		if (other.tag == "Health_pickup") {			
			if (HP != 10) {
				SoundManager.instance.PlaySingle (GetHpSound);
				if (HP + 2 > 10)
					HP = 10;
				else
					HP += 2;
				other.gameObject.SetActive (false);
			}
		}
		 

	}
}