using UnityEngine;
using System.Collections;

public class Victory : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			GameManager.level = 0;
			SoundManager.instance.BGMList [6].Stop ();
			Application.LoadLevel ("menu");
		}
	}
}
