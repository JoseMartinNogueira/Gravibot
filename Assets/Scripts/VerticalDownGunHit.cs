using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalDownGunHit : BulletHit {

	public float moveSpeedAplyed;
	// Use this for initialization
	public override void hitManager ()
	{
		movementBullets();
	}

	public override bool allawedMove (BoxCollition bc)
	{
		return !bc.isOnTheGround();
	}
	public override void specificMovement (Rigidbody2D collider )
	{
		collider.velocity = new Vector2(0, moveSpeedAplyed);
	}
}
