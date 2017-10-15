using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollition : MonoBehaviour {

	// Use this for initialization
	bool grounded;
	bool leftWall;
	bool ceiling;
	bool rightWall;
	Rigidbody2D box;
	void Awake () {
		box = GetComponent<Rigidbody2D>();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == 8) {
			if (box.velocity.y < 0) {
				grounded = true;
				ceiling = false;
				leftWall = false;
				rightWall = false;
			} else if (box.velocity.y > 0) {
				ceiling = true;
				grounded = false;
				leftWall = false;
				rightWall = false;
			} if (box.velocity.x < 0) {
				leftWall = true;
				ceiling = false;
				grounded = false;
				rightWall = false;
			} else if (box.velocity.x > 0) {
				rightWall = true;
				ceiling = false;
				grounded = false;
				leftWall = false;
			}
			if (box.isKinematic) {
				box.velocity = new Vector2 (0, 0);
			}
		}
	}
	void OnTriggerStay2D (Collider2D other)
	{
	}
	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.layer == 8) {
			if (box.velocity.y < 0) {
				ceiling = false;
			} else if (box.velocity.y > 0) {
				grounded = false;
			}  if (box.velocity.x < 0) {
				rightWall = false;
			} else if (box.velocity.x > 0) {
				leftWall = false;
			}

		}
	}
	public bool isOnTheGround ()
	{
		return grounded;
	}

	public bool isOnALeftWall ()
	{
		return leftWall;
	}

	public bool isOnARightWall ()
	{
		return rightWall;
	}

	public bool isOnTheCeiling ()
	{
		return ceiling;
	}

	// Update is called once per frame
	void Update ()
	{
		
	}
}
