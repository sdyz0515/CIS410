using UnityEngine;
using System.Collections;

public class Move_E_fish : MonoBehaviour
{
	private bool faceright;
	public float speed = 5f;
	private int x_direction = 1;
	private Rigidbody2D rd2d;

	void Start () {  	
		Weapon_fish weapon = GetComponentInParent<Weapon_fish> ();
		if (weapon == null) {
			Debug.LogError ("Fish Weapon is null!");
			return;
		}
		Fish fish = weapon.GetComponentInParent<Fish> ();
		if (fish == null) {
			Debug.LogError ("Fish is null!");
			return;
		}
		faceright = fish.facingRight;

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