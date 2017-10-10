using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalDownGunHit : MonoBehaviour {

	// Use this for initialization
	BulletController bulletController;
	bool once;
	Collider2D target;

	void Awake () 
	{
		bulletController = GetComponent<BulletController>();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag != "Player") {
			once = true;
			target = other;
		}
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
				BoxCollition bc = target.GetComponent<BoxCollition> ();
				if (!bc.isOnTheGround()) {
					if (collider.isKinematic) {
						collider.velocity = new Vector2 (0, -1);
					} else {
						collider.isKinematic = true;
						collider.velocity = new Vector2 (0, -1);
					}
				}
			}
			once = false;
			Destroy(gameObject);
		}
	}
}
