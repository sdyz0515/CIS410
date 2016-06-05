using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public GameObject deadParticle;
	public PlayerController player;
	private List<Enemy> enemies;
	private bool restart = false;

	public int enemy_num = 2;
	public Enemy[] enemyTiles;
	public AudioClip gameOverSound;
	public static int level = 0;
	public static int[] Ability_List = {0,0,0};
	public static int Ability_Index = 0;
	public static int Ability_num = 1;
	public static bool SlimeDead;

	void Start () {
		Time.timeScale = 1;
		//pausePanel.SetActive (false);
		//pauseButton.SetActive (true);

	}


	public void OnPause()
	{
		//pausePanel.SetActive (true);
		//pauseButton.SetActive (false);
		Time.timeScale = 0;
	}

	public void UnPause()
	{
		//pausePanel.SetActive (false);
		//pauseButton.SetActive (true);
		Time.timeScale = 1;
	}
	void Awake()
	{  
		//r_button.gameObject.SetActive (false);
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

	}

	public void GameOver()
	{	
		SoundManager.instance.PlaySingle (gameOverSound);
		Instantiate(deadParticle, gameObject.transform.position, gameObject.transform.rotation);
		enabled = false;
		restart = true;
	}


	public void AddEnemyToList(Enemy script)
	{
		enemies.Add (script);
	}
		
	void Update () {
		if (PlayerController.HP == 0)
			GameOver ();
	}
}
