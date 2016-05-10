using UnityEngine;
using System.Collections;

public class Weapon_dragon : MonoBehaviour {

	public GameObject shot;
	public float fireRate;
	public float delay;

	void Start ()
	{	
		InvokeRepeating ("Fire_dragon", delay, fireRate);
	}

	void Fire_dragon()
	{
		Instantiate (shot, transform.position, transform.rotation);
	}

	void Fire_bat ()
	{
		StartCoroutine("delayCoroutine");   
	}

	IEnumerator delayCoroutine()
	{
		Instantiate (shot, transform.position, transform.rotation);
		yield return new WaitForSeconds(0.1f);
		Instantiate (shot, transform.position, transform.rotation);
		yield return new WaitForSeconds(0.1f);
		Instantiate (shot, transform.position, transform.rotation);
	}
}
