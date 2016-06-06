using UnityEngine;
using System.Collections;

public class shield : MonoBehaviour {

	public Vector2 offset;

	private Vector2 pos;

	void Start () {
	}

	void Update () {
		if (PlayerController.facingRight) {
			pos = ((Vector2)GameObject.FindGameObjectWithTag("Player").transform.position) + (offset);
		}
		else {
			pos = ((Vector2)GameObject.FindGameObjectWithTag("Player").transform.position) - (offset);
		}
		transform.position = pos;

	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Enemy_Bolt"){
			Destroy (other.gameObject);
		}
	}
}