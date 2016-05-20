using UnityEngine;
using System.Collections;

public class Weapon_dragon_G : MonoBehaviour {

	public GameObject shot;
	public float fireRate;
	public float delay;
	static bool faceright;

	void Start ()
	{	
		//faceright = this.transform.GetComponentsInParent;
		InvokeRepeating ("Fire_G", delay, fireRate);
	}

	void Fire_dragon()
	{
		Instantiate (shot, transform.position, transform.rotation);
	}

	void Fire_G ()
	{
		StartCoroutine("delayCoroutine");   
	}

	IEnumerator delayCoroutine()
	{
		Instantiate (shot, transform.position, transform.rotation);
		yield return new WaitForSeconds(0.2f);
		Instantiate (shot, transform.position, transform.rotation);
		yield return new WaitForSeconds(0.2f);
		Instantiate (shot, transform.position, transform.rotation);
	}
}
