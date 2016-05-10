using UnityEngine;
using System.Collections;

public class Dragon_Enemy : Enemy {

	private Transform target;
	private int Hp = 2;
	public static bool facingRight = true;

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

		float offset = player.transform.position.x - transform.position.x;

		if (offset > float.Epsilon && !facingRight) 
			Flip ();
		else if (offset < float.Epsilon && facingRight)
			Flip ();

		if (Mathf.Abs (target.position.x - transform.position.x) > float.Epsilon)
			x = target.position.x > transform.position.x ? 1 : -1;
		Move (x, 0);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Bolt") || other.CompareTag ("Fire_Bolt")) 
		{
			Hp--;
			Destroy (other.gameObject);
			SoundManager.instance.PlaySingle (enemyHitSound);
			if (Hp == 0) {
				GameObject deadcopy = Instantiate (dead, transform.position, transform.rotation) as GameObject;
				Destroy (deadcopy, 1);
				Destroy (gameObject);
			} 
			else 
			{
				GameObject hitcopy = Instantiate (hit, transform.position, transform.rotation) as GameObject;
				Destroy (hitcopy, 0.5f);
			}
		}
	}
}
