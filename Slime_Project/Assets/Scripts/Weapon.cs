using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour {
	public float fireRate;
	public List<GameObject> shots;
	private float nextFire;
	public static int fireMode = 0;
	public static bool disable_weapon = false;
	public Vector2 offset;

	private Vector2 pos;

	void Update() 
	{	
		if ((Input.GetButton ("Fire1") || Input.GetKey(KeyCode.Z)) && Time.time > nextFire && !disable_weapon) 
		{
			nextFire = Time.time + fireRate;
			if (fireMode == 3) {
				StartCoroutine (eShield ());
			} else if (fireMode == 5) {
				StartCoroutine (Shield ());
			}else {
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

	IEnumerator eShield() 
	{	
		pos = GameObject.FindGameObjectWithTag("Player").transform.position;
		GameObject eShield = Instantiate (shots[fireMode], pos, transform.rotation) as GameObject;
		yield return new WaitForSeconds (0.8f);
		Destroy (eShield);
	}

	IEnumerator Shield()
	{
		if (PlayerController.facingRight) {
			pos = ((Vector2)GameObject.FindGameObjectWithTag("Player").transform.position) + (offset);
		}
		else {
			pos = ((Vector2)GameObject.FindGameObjectWithTag("Player").transform.position) - (offset);
		}
		GameObject shield = Instantiate (shots[fireMode], pos, transform.rotation) as GameObject;
		yield return new WaitForSeconds (1f);
		Destroy (shield);

	}


}