using UnityEngine;
using System.Collections;

public class Electric_Shield : MonoBehaviour {

	void Start () {
	}

	void Update () {
		transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position;
	}
}
