using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour {

	// Use this for initialization

	BulletController bulletController;

	void Awake () 
	{
		bulletController = GetComponent<BulletController>();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			bulletController.removeForce();
			Destroy(gameObject);
		}
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			bulletController.removeForce();
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
