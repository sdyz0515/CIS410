using UnityEngine;
using System.Collections;

public class Move_boss_bolt : MonoBehaviour {

	private bool faceright;
	public float speed;
	public float range = 13.0f;
	private int x_direction = 1;
	private Rigidbody2D rd2d;
	private Vector2 inti_postion;

	void Start ()
	{  	
		inti_postion = transform.position;

		faceright = BossController.facingRight;
		if (faceright)
			x_direction = 1;
		else
			x_direction = -1;
		rd2d = GetComponent<Rigidbody2D> ();

		Vector3 theScale = transform.localScale;
		theScale.x *= x_direction;
		transform.localScale = theScale;

		Vector2 movement = new Vector2 (x_direction * speed, rd2d.velocity.y);
		rd2d.velocity = movement;
	}

	void Update() 
	{	
		print(inti_postion.x- transform.position.x);
		faceright = BossController.facingRight;
		if ((inti_postion.x - transform.position.x) * (inti_postion.x - transform.position.x) >= range * range) {
			Destroy (gameObject);
		}
	}
		




}
