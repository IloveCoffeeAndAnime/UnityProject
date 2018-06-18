using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenWinWindow : MonoBehaviour {

	void Start () {
	}

	void OnTriggerEnter2D(Collider2D collider) {
		WindowsController.current.initWinWindow ();
	}
		
}
