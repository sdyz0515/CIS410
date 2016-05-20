using UnityEngine;
using System.Collections;

public class Enemy_Frog : Enemy {

	public LayerMask whatIsGround;
	public SpriteRenderer renderer;
	public float jumpForce = 200f;
	public Transform groundCheck;
	public float JumpRate;

	private bool grounded = false;
	private bool isJumping = false;
	private Animator animator;
	private float groundRadius = 0.2f;
	private float nextJump;

	private Transform target;
	private float Hp = 3.0f;
	private bool facingRight_1 = true;
	private string status;

	protected override void Start () 
	{
		animator = GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		bolt_num = 1;
		base.Start ();
	}

	void Jump(float x) 
	{	
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		if (grounded) 
		{   isJumping = false;
			if (isJumping == false) {
				rb2d.AddForce (new Vector2 (x, jumpForce));
				//animator.SetTrigger ("Jump");
				isJumping = true;
			}
		}
	}
		
	void Flip () 
	{
		facingRight_1 = !facingRight_1;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void FixedUpdate () {
		
		float x = 0.0f;
		float offset = player.transform.position.x - transform.position.x;
		if (Mathf.Abs (offset) <= 5) {
			if (offset > float.Epsilon && !facingRight_1)
				Flip ();
			else if (offset < float.Epsilon && facingRight_1)
				Flip ();

			if (Mathf.Abs (target.position.x - transform.position.x) > float.Epsilon)
				x = target.position.x > transform.position.x ? 30f : -30f;
		} 
		else {
			x = facingRight_1 ? 40f : -40f;
		}
			
		if( Time.time > nextJump) 
		{
			nextJump = Time.time + JumpRate;
			Jump(x); 
		}

		switch (status){
			case "burn":
				Hp -= 0.01f;
				if (Hp <= 0) {
					GameObject deadcopy = Instantiate (dead, transform.position, transform.rotation) as GameObject;
					Destroy (deadcopy, 1);
					Destroy (gameObject);
				} 
				break;

			default:
				break;
		}


	}

	private void OnTriggerEnter2D(Collider2D other)
	{   
		switch (other.tag) {
		case "Bound":
			Flip ();
			break;

		case "Bolt":
			Hp-= 1.0f;
			Destroy (other.gameObject);
			SoundManager.instance.PlaySingle (enemyHitSound);
			Death (Hp,gameObject);
			break;


		case "Fire_Bolt":
			Hp-= 1.0f;
			Destroy (other.gameObject);
			SoundManager.instance.PlaySingle (enemyHitSound);
			status = "burn";
			renderer.color = Color.red;
			Death (Hp,gameObject);

			break;

		case "Ice_Bolt":
			Hp -= 0.5f;
			Destroy (other.gameObject);
			SoundManager.instance.PlaySingle (enemyHitSound);
			status = "iced";
			renderer.color = Color.blue;
			inverseMoveTime -= 0.5f;
			if (inverseMoveTime <= 0.0f)
				inverseMoveTime = 0.0f;
			Death (Hp,gameObject);

			break;

		case "Electric_Shield":
			Hp-= 0.5f;
			SoundManager.instance.PlaySingle (enemyHitSound);
			Death (Hp,gameObject);
			break;
		
		default:
			break;
		}
	}
}
