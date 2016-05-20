using UnityEngine;
using System.Collections;

public class generate_eBall : MonoBehaviour {

	public GameObject eBall;
	public float rate;
	public float delay;

	void Update() 
	{	
		if (Time.time > delay) {
			delay = Time.time + rate;
			Instantiate (eBall, transform.position, transform.rotation);
		}
	}
}
