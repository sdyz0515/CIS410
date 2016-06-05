using UnityEngine;
using System.Collections;

public class Jellyfish : Enemy {

	private Transform target;
	private float Hp = 2.0f;
	private string status;
	private float imt;
	private Rigidbody2D body;

	public SpriteRenderer renderer;
	public GameObject LargeBubble;

	protected override void Start () {
		body = GetComponent<Rigidbody2D> ();
		inverseMoveTime = 1.0f;
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		base.Start ();
	}

	void FixedUpdate () {
		int x = 0;
		int y = 0;

		float offset = player.transform.position.x - transform.position.x;

		if (Mathf.Abs (target.position.x - transform.position.x) > float.Epsilon)
			x = target.position.x > transform.position.x ? 1 : -1;

		if (Mathf.Abs (target.position.y - transform.position.y) > float.Epsilon)
			y = target.position.y > transform.position.y ? 1 : -1;

		Move (x, y);

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
