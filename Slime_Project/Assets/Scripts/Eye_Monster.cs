using UnityEngine;
using System.Collections;

public class Eye_Monster : Enemy {

	private Transform target;
	private float Hp = 2;
	private bool facingRight = true;
	private string status;
	public SpriteRenderer renderer;


	// Use this for initialization
	protected override void Start () {
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		base.Start ();
	}
		
	void Flip () 
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void FixedUpdate () {
		int x = 0;
		int y = 0;

		float offset = player.transform.position.x - transform.position.x;

		if (offset > 0 && !facingRight) 
			Flip ();
		else if (offset < 0 && facingRight)
			Flip ();

		if (Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
			y = target.position.y > transform.position.y ? 1 : -1;
		else
			x = target.position.x > transform.position.x ? 1 : -1;
		Move (x, y);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{   
		switch (other.tag) {

		case "Bolt":
			Hp-= 1.0f;
			Destroy (other.gameObject);
			SoundManager.instance.PlaySingle (enemyHitSound);
			if (Hp  <= 0.0f) {
				GameObject deadcopy = Instantiate (dead, transform.position, transform.rotation) as GameObject;
				Destroy (deadcopy, 1);
				Destroy (gameObject);
			} else {
				GameObject hitcopy = Instantiate (hit, transform.position, transform.rotation) as GameObject;
				Destroy (hitcopy, 0.5f);
			}
			break;


		case "Fire_Bolt":
			Hp-= 1.0f;
			Destroy (other.gameObject);
			SoundManager.instance.PlaySingle (enemyHitSound);
			status = "burn";
			renderer.color = Color.red;
			if (Hp  <= 0.0f) {
				GameObject deadcopy = Instantiate (dead, transform.position, transform.rotation) as GameObject;
				Destroy (deadcopy, 1);
				Destroy (gameObject);
			} else {
				GameObject hitcopy = Instantiate (hit, transform.position, transform.rotation) as GameObject;
				Destroy (hitcopy, 0.5f);
			}

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
			if (Hp  <= 0.0f) {
				GameObject deadcopy = Instantiate (dead, transform.position, transform.rotation) as GameObject;
				Destroy (deadcopy, 1);
				Destroy (gameObject);
			} else {
				GameObject hitcopy = Instantiate (hit, transform.position, transform.rotation) as GameObject;
				Destroy (hitcopy, 0.5f);
			}

			break;

		default:
			break;
		}
	}
}
