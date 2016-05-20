using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {
	public GameObject dead;
	public GameObject hit;
	public AudioClip enemyHitSound;
	public static float inverseMoveTime = 2.0f;
	public static int bolt_num;

	protected GameObject player;
	protected CircleCollider2D circleCollider;
	protected Rigidbody2D rb2d;

	protected void Move(float xDir, float yDir)
	{
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);

		StartCoroutine (SmoothMovement (end));
	
	}

	protected virtual void Start() 
	{	
		circleCollider = GetComponent<CircleCollider2D> ();
		rb2d = GetComponent<Rigidbody2D> ();
		player = GameObject.FindWithTag ("Player");
	}

	protected IEnumerator SmoothMovement (Vector3 end)
	{
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

		while (sqrRemainingDistance > float.Epsilon) 
		{
			Vector3 newPosition = Vector3.MoveTowards (rb2d.position, end, inverseMoveTime * Time.deltaTime);
			rb2d.MovePosition (newPosition);
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
	}

	public void Death(float hp, GameObject obj){
		if (hp <= 0){
			GameObject deadcopy = Instantiate (dead, transform.position, transform.rotation) as GameObject;
			Destroy (deadcopy, 1);
			Destroy (obj);
			PlayerController.energy += 2;
		}
		else{
			GameObject hitcopy = Instantiate (hit, transform.position, transform.rotation) as GameObject;
			Destroy (hitcopy, 0.5f);
		}
	}
}
