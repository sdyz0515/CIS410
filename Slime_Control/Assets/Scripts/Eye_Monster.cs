using UnityEngine;
using System.Collections;

public class Eye_Monster : Enemy {

	private Transform target;
	private int HP;


	// Use this for initialization
	protected override void Start () {
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		HP = 1;
		base.Start ();

	}



	void FixedUpdate () {
		int x = 0;
		int y = 0;

		if (Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
			y = target.position.y > transform.position.y ? 1 : -1;
		else
			x = target.position.x > transform.position.x ? 1 : -1;
		Move (x, y);
	}
}
