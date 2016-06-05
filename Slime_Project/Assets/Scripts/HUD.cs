using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HUD : MonoBehaviour {
	public Sprite[] MeterSprites;
	public Image HeartUI;
	private PlayerController player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectsWithTag ("Player")[0].GetComponent<PlayerController> ();

	}

	// Update is called once per frame
	void Update () {
		HeartUI.sprite = MeterSprites [PlayerController.HP];


	}
}
