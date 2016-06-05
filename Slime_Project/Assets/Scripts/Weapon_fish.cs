using UnityEngine;
using System.Collections;

public class Weapon_fish : MonoBehaviour {

	public GameObject shot;
	public float fireRate;
	public float delay;

	void Start ()
	{	
		InvokeRepeating ("Fire_fish", delay, fireRate);
	}

	void Fire_fish()
	{
		GameObject bolt = Instantiate (shot, transform.position, transform.rotation) as GameObject;
		bolt.transform.parent = transform;
	}

}
