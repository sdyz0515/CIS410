using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class AbilityHUD : MonoBehaviour {
	public Sprite[] MeterSprites;
	public Image HeartUI;
	private PlayerController player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectsWithTag ("Player")[0].GetComponent<PlayerController> ();

	}

	// Update is called once per frame
	void Update () {
		if (PlayerController.energy >= 6)
			PlayerController.energy = 6;
		HeartUI.sprite = MeterSprites [PlayerController.energy];


	}
}
