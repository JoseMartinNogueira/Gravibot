using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float bulletSpeed;
	Rigidbody2D bullet;
	// Use this for initialization
	void Awake () {
		bullet = GetComponent<Rigidbody2D>();

		bullet.AddForce( new Vector2(1,0)*bulletSpeed, ForceMode2D.Impulse ); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
