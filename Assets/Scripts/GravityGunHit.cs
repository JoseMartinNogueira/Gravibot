using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunHit : BulletHit {

	public override void hitManager ()
	{
		//bulletController.removeForce ();
		Rigidbody2D collider = target.GetComponent<Rigidbody2D> ();
		if (target.gameObject.tag == "Object" || target.gameObject.tag == "Bullet" ) {
			collider.isKinematic = true;
			specificMovement( collider );
		}
		once = false;
		if (target.gameObject.layer != 10) {
			Destroy(gameObject);
		}
	}
	public override bool allawedMove (BoxCollition bc)
	{
		return true;
	} 
	public override void specificMovement (Rigidbody2D collider )
	{
		collider.velocity = new Vector2 (0, 0);
	}

	public override void changeDirection (Rigidbody2D bullet)
	{
		bullet.gameObject.transform.localRotation = bullet.gameObject.transform.localRotation;
	}
}

