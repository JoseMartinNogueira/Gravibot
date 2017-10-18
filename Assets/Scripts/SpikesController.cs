using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : MonoBehaviour {

	static string loseScreen = "LoseScreen";
	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			Application.LoadLevel( loseScreen );
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
