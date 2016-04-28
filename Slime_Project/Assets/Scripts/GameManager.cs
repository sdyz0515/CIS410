using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public PlayerController player;
	private List<Enemy> enemies;
	public int enemy_num = 2;
	public Enemy[] enemyTiles;
	public Text gameOverText;
	public Text EatingText;
	public Text HPText;





	void Awake()
	{  
		
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		//DontDestroyOnLoad (gameObject);
		enemies = new List<Enemy> ();
		for (int i = 0; i < enemy_num; i++) {
			Enemy enemy = enemyTiles [Random.Range (0, enemyTiles.Length)];
			Instantiate (enemy,new Vector3(4.87f,-1.95f,0.00f),Quaternion.identity);

		}
		InitGame ();
	}

	void InitGame()
	{	

	}

	public void GameOver()
	{	
		gameOverText.text = "Game Over";
		enabled = false;
	}


	public void AddEnemyToList(Enemy script)
	{
		enemies.Add (script);
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		HPText.text = "HP: "+ player.HP;
		EatingText.text = "Eating CD: " + player.eatingCD;
		if (player.HP == 0)
			GameOver ();
		
	}
}
