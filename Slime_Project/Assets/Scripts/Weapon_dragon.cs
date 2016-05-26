using UnityEngine;
using System.Collections;

public class Weapon_dragon : MonoBehaviour {

	public GameObject shot;
	private GameObject ItMe;
	public float fireRate;
	public float delay;

	void Start ()
	{	
		InvokeRepeating ("Fire_dragon", delay, fireRate);
	}

	void Fire_dragon()
	{
		GameObject bolt = Instantiate (shot, transform.position, transform.rotation) as GameObject;
		bolt.transform.parent = transform;
	}

}
