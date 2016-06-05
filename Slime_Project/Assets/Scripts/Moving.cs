using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {

	public float speed;

	void Start ()
	{
		//GetComponent<Rigidbody>().velocity = transform.forward * speed;
	
	}
	void Update(){
		//transform.position += new Vector2 (Input.GetAxis ("Horizontal;"), 0) * speed * Time.deltaTime;

		float x = Input.GetAxis ("Horizontal");

		transform.position = new Vector2 (transform.position.x - speed * Time.deltaTime, transform.position.y);
	}
}
