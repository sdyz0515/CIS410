using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class BossController: Enemy {

	private Transform target;
	private float Hp = 15.0f;
	public static bool facingRight = false;
	private bool faceright = false;
	private string status;
	public SpriteRenderer renderer;

	protected override void Start () {
		inverseMoveTime = 4;
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
		inverseMoveTime = 4;
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
		//inverseMoveTime = 4;
		Move (x, 0);

		switch (status){

		case "burn":
			Hp -= 0.005f;
			if (Hp <= 0) {
				Boss_Dead ();
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

		case "Bolt":
			Hp-= 1.0f;
			Destroy (other.gameObject);
			SoundManager.instance.PlaySingle (enemyHitSound);
			if (Hp <= 0){
				Boss_Dead ();
			}
			else{
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
			if (Hp <= 0){
				Boss_Dead ();
			}
			else{
				GameObject hitcopy = Instantiate (hit, transform.position, transform.rotation) as GameObject;
				Destroy (hitcopy, 0.5f);
			}
			break;

		case "Ice_Bolt":
			Hp -= 0.5f;
			Destroy (other.gameObject);
			SoundManager.instance.PlaySingle (enemyHitSound);
			if (Hp <= 0){
				Boss_Dead ();
			}
			else{
				GameObject hitcopy = Instantiate (hit, transform.position, transform.rotation) as GameObject;
				Destroy (hitcopy, 0.5f);
			}
			break;
	
		case "LeftBounder":
			if(!facingRight)
				inverseMoveTime = 0;
			break;
		
		case "RightBounder":
			if(facingRight)
				inverseMoveTime = 0;
			break;

		case "Electric_Shield":
			Hp-= 0.5f;
			SoundManager.instance.PlaySingle (enemyHitSound);
			if (Hp <= 0){
				Boss_Dead ();
			}
			else{
				GameObject hitcopy = Instantiate (hit, transform.position, transform.rotation) as GameObject;
				Destroy (hitcopy, 0.5f);
			}
			break;

		case "Bubble_Bolt":
			Destroy (other.gameObject);
			SoundManager.instance.PlaySingle (enemyHitSound);
			break;

		default:
			break;
		}
	}

	void Boss_Dead(){
		GameObject deadcopy = Instantiate (dead, transform.position, transform.rotation) as GameObject;
		Destroy (deadcopy, 1);
		SoundManager.instance.PlayNextBGM (6); // victory sound
		Destroy (gameObject);
		Application.LoadLevel ("Congrats");
	}
}
