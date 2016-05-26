using UnityEngine;
using System.Collections;

public class Move_E_Green : MonoBehaviour
{
	private bool faceright;
	public float speed;
	private int x_direction = 1;
	private Rigidbody2D rd2d;

	void Start ()
	{  	
		Weapon_dragon_G weapon = GetComponentInParent<Weapon_dragon_G> ();
		if (weapon == null) {
			Debug.LogError ("Dragon Weapon is null!");
			return;
		}
		Dragon_Enemy_Green dragon = weapon.GetComponentInParent<Dragon_Enemy_Green> ();
		if (dragon == null) {
			Debug.LogError ("Dragon is null!");
			return;
		}
		faceright = dragon.faceright;
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

		transform.parent = null;
	}
		
}