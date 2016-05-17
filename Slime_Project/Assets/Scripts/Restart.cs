using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

	public AudioSource MenuBGM;

	public void RestartButton()
	{
		Application.LoadLevel("Level_0");
		MenuBGM.Stop ();
		SoundManager.instance.PlayNextBGM (0);
	}
		
}