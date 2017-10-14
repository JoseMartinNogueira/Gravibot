﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalGunLeftHit : BulletHit {

	public float moveSpeedAplyed;
	public override void hitManager ()
	{
		movementBullets();
	}

	public override bool allawedMove (BoxCollition bc)
	{
		return !bc.isOnALeftWall();
	}

	public override void specificMovement (Rigidbody2D collider )
	{
		collider.velocity = new Vector2(moveSpeedAplyed, 0);
	}
}

