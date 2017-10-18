using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour {

	// Use this for initialization
	public int currentLevel;

	void Start () {
		
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			currentLevel++;
			Application.LoadLevel(currentLevel.ToString());
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
