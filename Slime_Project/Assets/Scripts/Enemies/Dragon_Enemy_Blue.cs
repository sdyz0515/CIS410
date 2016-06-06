using UnityEngine;
using System.Collections;

public class Dragon_Enemy_Blue : Enemy {

	private Transform target;
	private float Hp = 3.0f;
	public bool faceright = true;
	private string status;
	public SpriteRenderer renderer;
	public float speed = 5.0f;

	public GameObject LargeBubble;
	private Rigidbody2D body;

	protected override void Start () {
		body = GetComponent<Rigidbody2D> ();
		faceright = true;
		renderer.color = Color.magenta;
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
		float offset_y = player.transform.position.y - transform.position.y;
		if ((Mathf.Abs (offset) <= 5) && (Mathf.Abs(offset_y) <= 5)) {
			if (offset > float.Epsilon && !faceright) 
				Flip ();
			else if (offset < float.Epsilon && faceright)
				Flip ();

			if (Mathf.Abs (target.position.x - transform.position.x) > float.Epsilon)
				x = target.position.x > transform.position.x ? 1 : -1;
			inverseMoveTime = speed;
		} 
		else {
			x = faceright ? 4 : -4;
			inverseMoveTime = speed;
		}

		Move (x, 0);

		switch (status){

		case "burn":
			Hp -= 0.01f;
			if (Hp <= 0) {
				PlayerController.energy += 3;
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
			Hp-= 3f;
			SoundManager.instance.PlaySingle (enemyHitSound);
			Death (Hp,gameObject);
			break;

		case "Bubble_Bolt":
			Destroy (other.gameObject);
			SoundManager.instance.PlaySingle (enemyHitSound);
			StartCoroutine(freeze());
			break;

		default:
			break;
		}
	}

	IEnumerator freeze(){
		GameObject BubbleAround = Instantiate (LargeBubble, transform.position, transform.rotation) as GameObject;
		BubbleAround.transform.parent = transform;
		body.constraints = RigidbodyConstraints2D.FreezePosition;
		yield return new WaitForSeconds(2.0f);
		Destroy (BubbleAround);
		body.constraints = RigidbodyConstraints2D.None;
		body.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

}




