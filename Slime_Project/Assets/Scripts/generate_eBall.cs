using UnityEngine;
using System.Collections;

public class generate_eBall : MonoBehaviour {

	public GameObject eBall;
	public float rate;
	public float delay;

	void Update() 
	{	
		if (Time.time > delay) 
		{
			delay = Time.time + rate;
			StartCoroutine (GenerateB ());
		}
	}

	IEnumerator GenerateB()
	{
		GameObject new_eBall = Instantiate (eBall, transform.position, transform.rotation) as GameObject;

		yield return new WaitForSeconds (0.7f);

		Destroy (new_eBall);
	}
}
