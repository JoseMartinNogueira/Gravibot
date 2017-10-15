using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	public void LoadLevel (string name)
	{
		Application.LoadLevel( name);
	}

	public void quitRequest ()
	{
		Debug.Log("Quit requested ");
		Application.Quit();
	}
}
