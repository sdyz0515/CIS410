using UnityEngine;
using System.Collections;

public class Electric_Ball : MonoBehaviour {

	void Start () {
		StartCoroutine (DestroySelf());
	}

	IEnumerator DestroySelf()
	{
		yield return new WaitForSeconds (0.7f);

		Destroy (gameObject);
	}
}
