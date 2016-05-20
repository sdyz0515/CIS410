using UnityEngine;
using System.Collections;

public class Dragon_Enemy : Enemy {

	private Transform target;
	private float Hp = 3.0f;
	public static bool facingRight = true;
	private bool faceright = true;
	private string status;
	public SpriteRenderer renderer;

	protected override void Start () {
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		bolt_num = 1;
		base.Start ();
	}

	void Flip () 
	{
		faceright = !faceright;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void FixedUpdate () {
		float x = 0.0f;
		float offset = player.transform.position.x - transform.position.x;

		if (offset > float.Epsilon && !faceright) 
			Flip ();
		else if (offset < float.Epsilon && faceright)
			Flip ();
		
		if (Mathf.Abs (target.position.x - transform.position.x) > float.Epsilon)
			x = target.position.x > transform.position.x ? 1 : -1;
		Move (x, 0);

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

		facingRight = faceright;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{   
		switch (other.tag) {

		case "Bound":
			Flip ();
			break;

		case "Bolt":
			Hp -= 1.0f;
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




