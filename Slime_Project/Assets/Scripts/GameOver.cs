using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	private string[] Level_list = {"Level_0","Level_1","Level_2","Level_3","Level_4","Level_5"};

	void Update () {
		if (Input.GetKeyDown (KeyCode.Y)) {
			Application.LoadLevel (Level_list [GameManager.level]);
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			GameManager.level = 0;
			Application.LoadLevel ("menu");
		}
	}
}
