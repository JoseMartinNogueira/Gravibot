using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  BulletHit : MonoBehaviour {

	public BulletController bulletController;
	public bool once;
	public Collider2D target;
	public bool bulletHit;

	void Awake () 
	{
		bulletHit = false;
		bulletController = GetComponent<BulletController>();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag != "Player" || (other.gameObject.tag == "Bullet" && !bulletHit)) {
			once = true;
			target = other;
			if (other.gameObject.tag == "Bullet") {
				bulletHit = true;
			}
		}
	}

	void OnTriggerStay2D (Collider2D other)
	{

	}

	// Update is called once per frame
	void Update ()
	{
		if (once) {
			hitManager();
		}
	}
	public abstract  void hitManager ();

	public void movementBullets ()
	{
		bulletController.removeForce ();
		Rigidbody2D collider = target.GetComponent<Rigidbody2D> ();	
		if (target.gameObject.tag == "Object") {
			BoxCollition bc = target.GetComponent<BoxCollition> ();
			if (allawedMove (bc)) {
				if (!collider.isKinematic) {
					collider.isKinematic = true;
				}
				specificMovement (collider);
			}
		} else if (target.gameObject.tag == "Bullet") {
			
		}
		once = false;
		if (target.gameObject.tag != "Bullet") {
			Destroy(gameObject);
		}
	}
	public abstract void specificMovement( Rigidbody2D collider );

	public abstract bool allawedMove( BoxCollition bc );
}