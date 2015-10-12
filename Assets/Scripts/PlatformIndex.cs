using UnityEngine;
using System.Collections;

public class PlatformIndex : MonoBehaviour {

	public GameObject[] platforms;
	public float[] distances;
	public int number;
	public bool[] connection;
	public bool visited;
	public GameObject parObj;


	// Use this for initialization
	void Start () {

		int i = 0;

		foreach (GameObject p in platforms) {
		
			distances[i] = Vector2.Distance(this.transform.position, p.transform.position);
			i++;

		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
