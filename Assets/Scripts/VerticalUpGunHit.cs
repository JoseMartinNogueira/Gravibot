using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalUpGunHit : BulletHit {

	public float moveSpeedAplyed;
	public override void hitManager ()
	{
		movementBullets();
	}

	public override bool allawedMove (BoxCollition bc)
	{
		return !bc.isOnTheCeiling();
	}

	public override void specificMovement (Rigidbody2D collider )
	{
		collider.velocity = new Vector2(0, moveSpeedAplyed);
	}
}
