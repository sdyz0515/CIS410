using UnityEngine;
using System.Collections;

public class Movingtowards : MonoBehaviour {

	public float speed;

	void Start ()
	{
		

	}
	void Update(){
		

		float x = Input.GetAxis ("Horizontal");

		transform.position = new Vector2 (transform.position.x + speed * Time.deltaTime, transform.position.y);
	}
}
