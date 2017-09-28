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
	public GameObject gravityGun;
	public GameObject copyGun;

	Vector3 leftShootingCorrection = new Vector3(0,0.1f,0);
	Vector3 rightShootingCorrection = new Vector3(0,0.05f,0);

	private enum AmmunitionType 
	{
		BULLET,
		GRAVITY,
		COPY,
	};

	AmmunitionType ammunition;
	GameObject ammu;
	float fireRateBullet = 0.1f;
	float fireRateGravityGun = 0.5f;
	float fireRateCopyGun = 1f;
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
		ammunition = AmmunitionType.BULLET;
	}

	void Update ()
	{
		if (grounded && Input.GetAxis ("Jump") > 0) {
			grounded = false;
			animator.SetBool ("isOnTheGround", grounded);
			character.AddForce (new Vector2 (0, jumpHeight));
		}
		if (Input.GetKey (KeyCode.Alpha1)) {
			ammunition = AmmunitionType.BULLET;
		} else if (Input.GetKey (KeyCode.Alpha2)) {
			ammunition = AmmunitionType.GRAVITY;
		} else if (Input.GetKey (KeyCode.Alpha3)) {
			ammunition = AmmunitionType.COPY;
		}
		if (Input.GetAxis ("Fire1") > 0) {
			fire(ammunition);
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
		float mouse = Input.GetAxis ("Mouse X");
		if( mouse > 0.0 && !facingRight ) {
			flip();
		} else if( mouse < 0.0  && facingRight ) {
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

	void fire (AmmunitionType ammType)
	{
		
		float fireRate = 0;
		switch (ammType) {
		case AmmunitionType.BULLET:
			ammu = bullet;
			fireRate = fireRateBullet;
			break;
		case AmmunitionType.GRAVITY:
			ammu = gravityGun;
			fireRate = fireRateGravityGun;
			break;
		case AmmunitionType.COPY:
			ammu = copyGun;
			fireRate = fireRateCopyGun;
			break;
		}
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			if (facingRight) {
				Instantiate (ammu, gunTip.position + rightShootingCorrection, Quaternion.Euler (new Vector3 (0, 0, 0)));
			} else {
				Instantiate (ammu, gunTip.position-leftShootingCorrection, Quaternion.Euler (new Vector3 (0, 0, 180f)));
			}
		}
	}
}