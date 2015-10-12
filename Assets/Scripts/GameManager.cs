using UnityEngine;
using System.Collections;

//Prototype for the Game Manager class. This script will control things like saving, if we get around to it,
//level control, tracking of variables between stuff like rounds, setting initialization variables, and match settings. 
//Remember to fill me out!

public class GameManager : MonoBehaviour {



	// Use this for initialization
	void Start () {

		DontDestroyOnLoad (this);
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown ("ForceQuit")){
			Application.Quit();
		}

	
	}

	public static void ChooseLevel(string levelName){

		if (levelName == "Quit") {
		
			Application.Quit ();
		
		} else {
			Application.LoadLevel (levelName);
		}

	}

}
