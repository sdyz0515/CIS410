using UnityEngine;
using System.Collections;

public class Meteor_Creator: MonoBehaviour {

	public GameObject shot;
	private GameObject ItMe;


	void Start ()
	{	
		InvokeRepeating ("Meteor", Random.Range(1,3), Random.Range(3,7));
	}

	void Meteor()
	{
		GameObject bolt = Instantiate (shot, transform.position, transform.rotation) as GameObject;
		//bolt.transform.parent = transform;
	}

}
