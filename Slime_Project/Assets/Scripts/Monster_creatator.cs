using UnityEngine;
using System.Collections;

public class Monster_creatator : MonoBehaviour {


	public Enemy[] enemyTiles;


	// Use this for initialization
	void Start () {

		Enemy enemy = enemyTiles [Random.Range (0, enemyTiles.Length)];
		Instantiate (enemy,transform.position,Quaternion.identity);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
