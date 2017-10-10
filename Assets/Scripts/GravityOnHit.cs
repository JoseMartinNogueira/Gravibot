﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOnHit : MonoBehaviour {

	BulletController bulletController;
	Collider2D target;
	bool once;
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
			bulletController.removeForce ();
			Rigidbody2D collider = target.GetComponent<Rigidbody2D> ();
			if (target.gameObject.tag == "Object") {
				collider.isKinematic = false;
			}
			once = false;
			Destroy(gameObject);
		}
	}
}