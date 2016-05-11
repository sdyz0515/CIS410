using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
	public Button r_button;
	public AudioClip gameOverSound;
	public static int level = 0;

	private bool restart = false;

	void Start () {
		
	}
	void Awake()
	{  
		r_button.gameObject.SetActive (false);
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		//DontDestroyOnLoad (gameObject);
		enemies = new List<Enemy> ();
		for (int i = 0; i < enemy_num; i++) {
			Enemy enemy = enemyTiles [Random.Range (0, enemyTiles.Length)];
			//Instantiate (enemy,new Vector3(4.87f,-0.48f,0.00f),Quaternion.identity);

		}
		InitGame ();
	}

	void InitGame()
	{	

	}

	public void GameOver()
	{	
		SoundManager.instance.PlaySingle (gameOverSound);
		gameOverText.text = "Game Over";
		enabled = false;
		r_button.gameObject.SetActive (true);
		restart = true;
	}


	public void AddEnemyToList(Enemy script)
	{
		enemies.Add (script);
	}


	
	// Update is called once per frame
	void Update () {
		HPText.text = "HP: "+ player.HP;
		EatingText.text = "Eating CD: " + player.eatingCD;
		if (player.HP == 0)
			GameOver ();
		
	}
}
