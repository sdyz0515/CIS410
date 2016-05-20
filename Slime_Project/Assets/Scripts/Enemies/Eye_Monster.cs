using UnityEngine;
using System.Collections;

public class Eye_Monster : Enemy {

	private Transform target;
	private float Hp = 2.0f;
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

		if (Mathf.Abs (target.position.x - transform.position.x) > float.Epsilon)
			x = target.position.x > transform.position.x ? 1 : -1;

		if (Mathf.Abs (target.position.y - transform.position.y) > float.Epsilon)
			y = target.position.y > transform.position.y ? 1 : -1;

		Move (x, y);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{   
		switch (other.tag) {

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
