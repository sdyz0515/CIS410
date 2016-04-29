using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

	public void RestartButton()
	{
		Application.LoadLevel("Level_0");
	}
		
}