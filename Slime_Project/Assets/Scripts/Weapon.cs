using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour {
	public float fireRate;
	public List<GameObject> shots;
	private float nextFire;
	public static int fireMode = 0;

	void Update() 
	{	
		if ((Input.GetButton ("Fire1") || Input.GetKey(KeyCode.Z)) && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			if (fireMode == 3) {
				StartCoroutine (Shield ());
			} else {
				StartCoroutine (Shoot ());
			}
		}
	}

	IEnumerator Shoot() 
	{	
		GameObject shoot = Instantiate (shots[fireMode], transform.position, transform.rotation) as GameObject;
		yield return new WaitForSeconds (1);
		if (shoot != null)
			Destroy (shoot);
	}

	IEnumerator Shield() 
	{	
		GameObject eShield = Instantiate (shots[fireMode], GameObject.FindGameObjectWithTag("Player").transform.position, transform.rotation) as GameObject;
		yield return new WaitForSeconds (0.8f);
		Destroy (eShield);
	}
}
