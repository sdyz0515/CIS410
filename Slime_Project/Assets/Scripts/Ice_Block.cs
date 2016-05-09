using UnityEngine;
using System.Collections;

public class Ice_Block : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Bolt")) 
		{
			Destroy (other.gameObject);

			Destroy (gameObject);

		}
	}
}
