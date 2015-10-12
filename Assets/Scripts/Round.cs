using UnityEngine;
using System.Collections;

//This will be used to control all round settings eventually, keeping tracks of kills, deaths, etc.


public class Round : MonoBehaviour {

	public GameObject[] Players;
	public int deadPlayers;
	public int maxPlayers;
	public GameObject GameOverText;
	public bool roundStarted;


	// Use this for initialization
	void Start () {
	
		roundStarted = true;
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (roundStarted == true) {
			foreach (GameObject p in Players) {
				
				if (p.GetComponent<Fighter> ().health <= 0) {
					
					deadPlayers++;
					
				}
				
			}
			
			if (deadPlayers >= (maxPlayers - 1)) {
				
				roundStarted = false;
				StartCoroutine ("RoundEnd");
				
			}
		}


	}

	IEnumerator RoundEnd(){
		
		yield return new WaitForSeconds (3.0f);
		GameManager.ChooseLevel ("MainMenu");
		
	}

}
