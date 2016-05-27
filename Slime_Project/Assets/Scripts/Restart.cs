using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

	public AudioSource MenuBGM;

	public void RestartButton()
	{
		GameManager.Ability_num = 1;
		GameManager.Ability_Index = 0;
		for (int i = 0; i < 3; i++) {
			GameManager.Ability_List [i] = 0;
		}
		Application.LoadLevel("Level_0");
		MenuBGM.Stop ();
		SoundManager.instance.PlayNextBGM (0);

	}
		
}