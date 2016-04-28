using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {


	private CircleCollider2D circleCollider;
	private Rigidbody2D rb2d;
	public float inverseMoveTime = 2 ;
	protected void Move(int xDir, int yDir)
	{
		Vector2 start = transform.position;
		Vector2 end = start + new Vector2 (xDir, yDir);

		StartCoroutine (SmoothMovement (end));
	
	}

	protected virtual void Start() 
	{
		circleCollider = GetComponent<CircleCollider2D> ();
		rb2d = GetComponent<Rigidbody2D> ();
	
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

	private void OnTriggerEnter2D(Collider2D other)
	{
		
	}
}
