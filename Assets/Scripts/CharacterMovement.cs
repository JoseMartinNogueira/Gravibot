using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
	#region Variables
	#region Movement Variables
	public float maxSpeed;

	private Rigidbody2D character;
	private Animator animator;
	private bool facingRight;

	#endregion
	#endregion

	// Use this for initialization
	void Start () {
		character = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		facingRight = true;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		
		float move = Input.GetAxis ("Horizontal");
		character.velocity = new Vector2( move*maxSpeed, character.velocity.y );
		animator.SetFloat("speed", Mathf.Abs(move) );

		if( move > 0.0 && !facingRight ) {
			flip();
		} else if( move < 0.0 && facingRight ) {
			flip();
		}
		 
	}

	void flip ()
	{
		facingRight = !facingRight;
		Vector3 nScale = transform.localScale;
		nScale.x *= -1;
		transform.localScale = nScale;
		Vector3 nPos = transform.localPosition;
		if (facingRight) {
			nPos.x -= 0.2f;
		} else {
			nPos.x += 0.2f;
		}
		transform.localPosition = nPos;
	}
}
