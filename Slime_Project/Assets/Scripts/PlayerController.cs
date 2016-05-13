using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float maxSpeed = 10f;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float jumpForce = 200f;
	public int HP =1;
	public float invincible_time  = 1f;
	public SpriteRenderer renderer;
	public int eatingCD = 0;
	public static bool facingRight = true;
	public GameObject hurtParticle;

	private bool grounded = false;
	private Rigidbody2D rb2d;
	private float groundRadius = 0.2f;
	private int invincible = 1;
	private int direction = 1;
	private bool eating = false;
	private Animator animator;
	private bool isJumping = false;
	private bool isAte = false;
	private string[] Level_list = {"Level_0","Level_1","Level_2","Level_3","Level_4"};
	private bool ifdead = false;

	public AudioClip jumpSound;
	public AudioClip getHpSound;
	public AudioClip eatingModeSound;
	public AudioClip eatEnemySound;
	public AudioClip hitSound;

	void Start () 
	{
		animator = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();	
		ifdead = false;
	}

	void FixedUpdate () 
	{	if (!ifdead) {
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
			float move = Input.GetAxis ("Horizontal");
			Vector2 movement = new Vector2 (move * maxSpeed, rb2d.velocity.y);
			rb2d.velocity = movement;

			if (move > 0 && !facingRight) {
				direction = -1;
				Flip ();
			} 
			else if (move < 0 && facingRight) {
				direction = 1;
				Flip ();
			}
		}
	}

	void Update () 
	{   

		if (grounded) {
			isJumping = false;
		}

		if (grounded && Input.GetKeyDown (KeyCode.Space) && !ifdead) 
		{   
			if (isJumping == false) {
				rb2d.AddForce (new Vector2 (0, jumpForce));
				animator.SetTrigger ("Jump");
				isJumping = true;
				SoundManager.instance.PlaySingle (jumpSound);

			}

		}
		if (Input.GetKeyDown (KeyCode.C)) 
		{	
			if (eating == false && eatingCD == 0) {
				EatingMode (true);
				Invoke ("CancelEatingMode", 3);
				SoundManager.instance.PlaySingle (eatingModeSound);

			}
		}
		if (Input.GetKeyDown (KeyCode.X)) 
		{	
			GameManager.Ability_Index = (GameManager.Ability_Index + 1) % GameManager.Ability_num;
		}

		Weapon.fireMode = GameManager.Ability_List[GameManager.Ability_Index];

		if (eatingCD > 0)
			eatingCD--;
	}
	void loseHP()
	{
		if (HP > 0 && invincible != 0) {
			GameObject hurt = Instantiate (hurtParticle, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
			HP--;
			invincible = 1;
			EatingMode (false);
			Destroy (hurt, 1);
			SoundManager.instance.PlaySingle (hitSound);
		}
	}

	void EatingMode(bool status)
	{
		if (status) {
			animator.SetTrigger ("Eating");
			eatingCD = 300;
			renderer.color = Color.red;
			eating = true;

		} else {
			renderer.color = Color.white;
			eating = false;
		}
	}

	void CancelEatingMode(){
		renderer.color = Color.white;
		eating = false;
	}


	void Flip () 
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	void Add_ability(int num)
	{
		if (GameManager.Ability_num >= 3) {
			GameManager.Ability_List [0] = num;
		}
		else
			GameManager.Ability_List [GameManager.Ability_num] = num;
		GameManager.Ability_num++;
	}


	private void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.tag) {

		case "Dragon":
			if (eating) {
				Destroy (other.gameObject);
				Add_ability (1);
				EatingMode (false);
				SoundManager.instance.PlaySingle (eatEnemySound);
			} else {
				rb2d.AddForce (new Vector2 (direction * 10000f, 300f));
				isJumping = true;
				loseHP ();
			}
			break;
		
		
		case "Eye_Monster":
			if (eating) {
				Destroy (other.gameObject);
				Add_ability (2);
				EatingMode (false);
				SoundManager.instance.PlaySingle (eatEnemySound);
			} else {
				rb2d.AddForce (new Vector2 (direction * 10000f, 300f));
				isJumping = true;
				loseHP ();
			}
			break;


		case "Enemy_Bolt":
			Destroy (other.gameObject);
			rb2d.AddForce (new Vector2 (direction * 10000f, 300f));
			isJumping = true;
			loseHP ();
			break;

		case "Trap":
			rb2d.AddForce (new Vector2 (0f, 1000f));
			loseHP ();
			break;

		case "Health_pickup":
			if (HP != 10) {
				
				if (HP + 2 > 10)
					HP = 10;
				else
					HP += 2;
				other.gameObject.SetActive (false);
				SoundManager.instance.PlaySingle (getHpSound);
			}
			break;

		case "Exit":
			Restart ();
			break;

		case "Lava": 
			Renderer render = GetComponent<Renderer> ();
			render.enabled = false;
			ifdead = true;
			break;

		default:
			break;
		}

	}


	private void Restart()
	{	
		GameManager.level++;
		facingRight = true;
		Application.LoadLevel (Level_list[GameManager.level]);
		SoundManager.instance.PlayNextBGM (GameManager.level); // need change with load level
	}

	IEnumerator Hurt() {
		GameObject hurt = Instantiate(hurtParticle, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		yield return new WaitForSeconds(1.0f);
		Destroy (hurt);
	}
}