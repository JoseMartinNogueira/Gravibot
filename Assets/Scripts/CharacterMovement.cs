using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
	#region Variables
	#region Movement Variables
	public float maxSpeed;
	#endregion
	#region Jump Variables
	bool grounded = false;
	static float groundCheckRadius = 0.2f;

	public LayerMask groundLayer;
	public Transform groundCheck;
	public float jumpHeight; 
	#endregion
	#region Shooting Variables
	public Transform gunTip;
	public GameObject bullet;
	float fireRate = 0.5f;
	float nextFire = 0f;

	#endregion
	private Rigidbody2D character;
	private Animator animator;
	private bool facingRight;

	#endregion

	// Use this for initialization
	void Start () {
		character = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		facingRight = true;
	}

	void Update ()
	{
		if (grounded && Input.GetAxis ("Jump") > 0) {
			grounded = false;
			animator.SetBool ("isOnTheGround", grounded);
			character.AddForce (new Vector2 (0, jumpHeight));
		}

		if (Input.GetKey (KeyCode.C)) {
			fireBullet();
		}
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		groundChecker();

		movementFunction();


	}

	void movementFunction ()
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

	void groundChecker()
	{
		//check if we are in the ground - if no, then we are falling
		grounded = Physics2D.OverlapCircle( groundCheck.position, groundCheckRadius, groundLayer );
		animator.SetBool( "isOnTheGround", grounded );

	}

	void fireBullet ()
	{
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			if (facingRight) {
				Instantiate(bullet, gunTip.position, Quaternion.Euler( new Vector3(0,0,0) ) );
			}
		}
	}
}
