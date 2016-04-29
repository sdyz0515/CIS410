using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
	private static bool faceright;
	public float speed;
	private int x_direction = 1;
	private Rigidbody2D rd2d;

	void Start ()
	{  	
		
		faceright = PlayerController.facingRight;
		if (faceright)
			x_direction = 1;
		else
			x_direction = -1;
		rd2d = GetComponent<Rigidbody2D> ();
		Vector2 movement = new Vector2 (x_direction * speed, rd2d.velocity.y);
		rd2d.velocity = movement;
	}

	void Upate() 
	{
		faceright = PlayerController.facingRight;
	}
}