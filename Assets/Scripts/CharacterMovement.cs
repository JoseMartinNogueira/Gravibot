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
	static float groundCheckRadius = 0.025f;

	public LayerMask groundLayer;
	public Transform groundCheck;
	public float jumpHeight; 
	#endregion
	#region Shooting Variables
	public Transform gunTip;
	public GameObject horizontalGun;
	public GameObject horizontalLeftGun;
	public GameObject gravityGun;
	public GameObject gravityActiveGun;
	public GameObject verticalGun;
	public GameObject verticalDownGun;

	Vector3 leftShootingCorrection = new Vector3(0,0.1f,0);
	Vector3 rightShootingCorrection = new Vector3(0,0.05f,0);

	bool shootingSwitch;

	private enum AmmunitionType 
	{
		VERTICAL,
		GRAVITY,
		HORIZONTAL,
	};

	AmmunitionType ammunition;
	GameObject ammu;
	float fireRateGravityGun = 0.5f;
	float fireRateCopyGun = 0.3f;
	float nextFire = 0f;

	public GameObject gunMenu;
	bool isShowingMenu;

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
		ammunition = AmmunitionType.HORIZONTAL;
		fixBulletSpeeds();
		gunMenu.SetActive(false);
		isShowingMenu = false;
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.X) && shootingSwitch) {
			isShowingMenu = !isShowingMenu;
			gunMenu.SetActive (isShowingMenu); 
		}
		if (!isShowingMenu) {
			
			if (Input.GetKey (KeyCode.Space)) {
				if (grounded) {
					grounded = false;
					animator.SetBool ("isOnTheGround", grounded);
					character.AddForce (new Vector2 (0, jumpHeight));
				} 
			}
			if (Input.GetKey (KeyCode.Alpha1)) {
				ammunition = AmmunitionType.HORIZONTAL;
			} else if (Input.GetKey (KeyCode.Alpha2)) {
				ammunition = AmmunitionType.VERTICAL;
			} else if (Input.GetKey (KeyCode.Alpha3)) {
				ammunition = AmmunitionType.GRAVITY;
			}
			if (Input.GetAxis ("Fire1") > 0) {
				fire (ammunition, 1);
			}
			if (Input.GetAxis ("Fire2") > 0) {
				fire (ammunition, 2);
			}

		}
			 
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (!isShowingMenu) {
			groundChecker ();

			movementFunction ();
		}

	}

	void movementFunction ()
	{
		float move = Input.GetAxis ("Horizontal");
		character.velocity = new Vector2( move*maxSpeed, character.velocity.y );
		animator.SetFloat("speed", Mathf.Abs(move) );
		float mouse = Input.GetAxis ("Mouse X");
		//float xCharacter = character.transform.localPosition.x;
		if( mouse > 0.2 && !facingRight ) {
			flip();
		} else if( mouse < -0.2  && facingRight ) {
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

	void fire (AmmunitionType ammType, int button)
	{
		
		float fireRate = 0;
		switch (ammType) {
		case AmmunitionType.VERTICAL:
			ammu = ( button == 1)? verticalGun : verticalDownGun;
			fireRate = fireRateCopyGun;
			break;
		case AmmunitionType.GRAVITY:
			ammu = (button == 1 )? gravityGun : gravityActiveGun;
			fireRate = fireRateCopyGun;
			break;
		case AmmunitionType.HORIZONTAL:
			ammu = (button == 1 )? horizontalGun : horizontalLeftGun;
			fireRate = fireRateCopyGun;
			break;
		}
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			if (facingRight) {
				Instantiate (ammu, gunTip.position + rightShootingCorrection, Quaternion.Euler (new Vector3 (0, 0, 0)));
			} else {
				Instantiate (ammu, gunTip.position, Quaternion.Euler (new Vector3 (0, 0, 180f)));
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Switch") {
			shootingSwitch = true;
		}
	}
	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == "Switch") {
			shootingSwitch = false;
		}
	}

	public void changeHorizontalLeftGun (float speedB)
	{
		BulletController bCont = horizontalLeftGun.GetComponent<BulletController>();
		bCont.bulletSpeed = speedB;
	}
	public void changeHorizontalRightGun (float speedB)
	{
		BulletController bCont = horizontalGun.GetComponent<BulletController>();
		bCont.bulletSpeed = speedB;
	}

	public void changeVerticalUpGun (float speedB)
	{
		BulletController bCont = verticalGun.GetComponent<BulletController>();
		bCont.bulletSpeed = speedB;
	}
	public void changeVerticalDownGun (float speedB)
	{
		BulletController bCont = verticalDownGun.GetComponent<BulletController>();
		bCont.bulletSpeed = speedB;
	}

	public void changeGravityOffGun( float speedB) 
	{
		BulletController bCont = gravityGun.GetComponent<BulletController>();
		bCont.bulletSpeed = speedB;
	}

	public void changeGravityOnGun( float speedB) 
	{
		BulletController bCont = gravityActiveGun.GetComponent<BulletController>();
		bCont.bulletSpeed = speedB;
	}

	void fixBulletSpeeds() 
	{
		changeHorizontalLeftGun(5);
		changeHorizontalRightGun(5);
		changeVerticalUpGun(5);
		changeVerticalDownGun(5);
		changeGravityOffGun(5);
		changeGravityOnGun(5);			
	}
}