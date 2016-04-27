using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float maxSpeed = 10f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float jumpForce = 200f;

	private bool facingRight = true;
	private bool grounded = false;
	private Rigidbody2D rb2d;
	private float groundRadius = 0.2f;

	void Start () 
	{
		rb2d = GetComponent<Rigidbody2D> ();	
	}

	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		float move = Input.GetAxis ("Horizontal");
		Vector2 movement = new Vector2 (move * maxSpeed, rb2d.velocity.y);
		rb2d.velocity = movement;

		if (move > 0 && !facingRight)
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();

	}

	void Update () 
	{
		if (grounded && Input.GetKeyDown (KeyCode.Space)) 
		{
			rb2d.AddForce (new Vector2 (0, jumpForce));
		}
	}

	void Flip () 
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
