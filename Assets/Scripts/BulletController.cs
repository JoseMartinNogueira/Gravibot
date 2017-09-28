using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	
	public float bulletSpeed;
	private Rigidbody2D bullet;
		
	// Use this for initialization
	void Awake ()
	{
		bullet = GetComponent<Rigidbody2D> ();
		if (transform.localRotation.z > 0) {
	
			bullet.AddForce (new Vector2 (-1, 0) * bulletSpeed, ForceMode2D.Impulse);
		} else {
			bullet.AddForce (new Vector2 (1, 0) * bulletSpeed, ForceMode2D.Impulse); 
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void removeForce() {
		bullet.velocity = new Vector2(0,0);
	}
}
