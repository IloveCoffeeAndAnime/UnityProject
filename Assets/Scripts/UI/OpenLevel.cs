using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenLevel : MonoBehaviour {

	public int level;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D collider) {
		if (level == 1)
			SceneManager.LoadScene ("Level01");
		else 
		SceneManager.LoadScene ("Level02");
	}
}
