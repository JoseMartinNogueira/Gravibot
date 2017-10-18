using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  BulletHit : MonoBehaviour {

	public BulletController bulletController;
	public bool once;
	public Collider2D target;
	static int bulletsLayer = 10;
	public float bulletSpeed;

	void Awake () 
	{
		bulletController = GetComponent<BulletController>();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag != "Player" && other.gameObject.tag != "Switch" && other.gameObject.tag != "Spikes") {
			once = true;
			target = other;
			if (other.gameObject.layer == 10) {
				Rigidbody2D thisBullet = other.GetComponent<Rigidbody2D>();
				specificMovement( thisBullet );
				changeDirection( thisBullet );
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
		Rigidbody2D collider = target.GetComponent<Rigidbody2D> ();	
		if (target.gameObject.tag == "Object") {
			BoxCollition bc = target.GetComponent<BoxCollition> ();
			if (allawedMove (bc)) {
				if (!collider.isKinematic) {
					collider.isKinematic = true;
				}
				specificMovement (collider);
			}
		} 
		once = false;
		if (target.gameObject.layer != bulletsLayer) {
			Destroy(gameObject);
		}
	}
	public abstract void specificMovement( Rigidbody2D collider );

	public abstract bool allawedMove( BoxCollition bc );

	public abstract void changeDirection( Rigidbody2D bullet );

}