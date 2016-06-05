using UnityEngine;
using System.Collections;

public class MushroomFlash : MonoBehaviour {

	private Color[] colors = {Color.cyan, Color.white, Color.yellow};
	void Start () {
		StartCoroutine(Flash(0.1f));
	}
	
	IEnumerator Flash(float intervalTime)
	{	int index = 0;
		while(true)
		{	
			gameObject.GetComponent<Renderer> ().material.color = colors[index % 3];
			index++;
			if (index > 1000)
				index = 0;
			yield return new WaitForSeconds(intervalTime);
		}
	}
}
