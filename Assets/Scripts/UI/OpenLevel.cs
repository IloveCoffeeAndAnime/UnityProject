using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenLevel : MonoBehaviour {
	public string scene;
	LevelDoorLook levelDoorLook;

	void Awake(){
	}
	// Use this for initialization
	void Start () {
		levelDoorLook = this.gameObject.GetComponent<LevelDoorLook> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D collider) {
		if (!levelDoorLook.IsLocked ()) {
			//SceneManager.LoadScene (scene);
		}
	}
}
