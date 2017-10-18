using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	int currentLevel = 1;

	public void LoadLevel (string name)
	{
		Application.LoadLevel( name);
		currentLevel = int.Parse(name);
	}

	public void quitRequest ()
	{
		Debug.Log("Quit requested ");
		Application.Quit();
	}
	public void nextLevel() {
		currentLevel++;
		Application.LoadLevel("2");
	}
}
