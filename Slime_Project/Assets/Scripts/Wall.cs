using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Bolt") || other.CompareTag ("Fire_Bolt") || other.CompareTag("Enemy_Bolt")) 
		{
			Destroy (other.gameObject);
		}
	}
}
