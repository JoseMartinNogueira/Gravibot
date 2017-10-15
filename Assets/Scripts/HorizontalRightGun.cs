using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalRightGun : BulletHit {

	public float moveSpeedAplyed;
	public override void hitManager ()
	{
		movementBullets();
	}
	public override bool allawedMove (BoxCollition bc)
	{
		return !bc.isOnARightWall();
	}
	public override void specificMovement (Rigidbody2D collider )
	{
		collider.velocity = new Vector2(moveSpeedAplyed, 0);
	}
	public override void changeDirection (Rigidbody2D bullet)
	{
		Quaternion euler = bullet.gameObject.transform.rotation;
		euler.z = 0;
		bullet.gameObject.transform.rotation = euler;
	}
}
