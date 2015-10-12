using UnityEngine;
using System.Collections;

public class StarPickup : MonoBehaviour {

	public GameObject StarSpawner;

	void OnTriggerEnter2D(Collider2D coll){

		//Star detects collision with player and does stuff
		if (coll.tag == "Player") {
			if (coll.GetComponent<Fighter> ().stars < coll.GetComponent<Fighter> ().starMax) {
				coll.GetComponent<Fighter> ().stars++;
			}
			StarSpawner.GetComponent<StarSpawn> ().curStars--;
			Destroy (this.gameObject);
		}
	}

}
