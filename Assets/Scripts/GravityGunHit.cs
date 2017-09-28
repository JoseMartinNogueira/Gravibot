using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunHit : MonoBehaviour {

	BulletController bulletController;
	Collider2D target;
	bool once;
	void Awake () 
	{
		bulletController = GetComponent<BulletController>();

	}

	void OnTriggerEnter2D (Collider2D other)
	{
		target = other;
		once = true;
	}

	void OnTriggerStay2D (Collider2D other)
	{
	}

	// Update is called once per frame
	void Update ()
	{
		if (once) {
			bulletController.removeForce();
			Rigidbody2D collider = target.GetComponent<Rigidbody2D> ();
			if (target.gameObject.tag == "Object" ) {
				if (collider.gravityScale == 0) {
					collider.gravityScale = 1;
					collider.freezeRotation = false;
					collider.constraints = RigidbodyConstraints2D.None;
				} else {
					collider.gravityScale = 0;
					collider.freezeRotation = true;
					collider.constraints = RigidbodyConstraints2D.FreezeAll;
				}
			}
			once = false;
			Destroy(gameObject);
		}
	}
}
