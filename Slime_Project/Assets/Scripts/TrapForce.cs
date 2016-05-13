using UnityEngine;
using System.Collections;

public class TrapForce : MonoBehaviour {
	public int direction;
	private void OnTriggerEnter2D(Collider2D other) 
	{
		Rigidbody2D player = other.GetComponent<Rigidbody2D> ();
		if (other.tag == "Player") {
			switch (direction) {
			case 1:
				player.AddForce (new Vector2 (0f, 1000f));
				break;
			case 2:
				player.AddForce (new Vector2 (0f, -1000f));
				break;
			case 3:
				player.AddForce (new Vector2 (5000f, 0f));
				break;
			case 4:
				player.AddForce (new Vector2 (-5000f, 0f));
				break;
			default:
				break;
			}
		}
	}
}
