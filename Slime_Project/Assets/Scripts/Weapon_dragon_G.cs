using UnityEngine;
using System.Collections;

public class Weapon_dragon_G : MonoBehaviour {

	public GameObject shot;
	public float fireRate;
	public float delay;
	static bool faceright;

	void Start ()
	{	
		InvokeRepeating ("Fire_G", delay, fireRate);
	}

	void Fire_G ()
	{
		StartCoroutine("delayCoroutine");   
	}

	IEnumerator delayCoroutine()
	{
		GameObject bolt = Instantiate (shot, transform.position, transform.rotation) as GameObject;
		bolt.transform.parent = transform;
		yield return new WaitForSeconds(0.2f);
		GameObject bolt_1 = Instantiate (shot, transform.position, transform.rotation) as GameObject;
		bolt_1.transform.parent = transform;
		yield return new WaitForSeconds(0.2f);
		GameObject bolt_2 = Instantiate (shot, transform.position, transform.rotation) as GameObject;
		bolt_2.transform.parent = transform;
	}
}
