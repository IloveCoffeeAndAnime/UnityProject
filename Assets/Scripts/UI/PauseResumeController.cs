using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseResumeController : MonoBehaviour {
	public static bool isPaused = false;
	public GameObject settingsWindow;
	public GameObject mainCanvas;
	// Use this for initialization
	void Start () {
		settingsWindow.SetActive (false);
		mainCanvas.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
			{
			if (isPaused)
				resume ();
			else
				pause ();
			}
	}

	public void pause()
	{
		settingsWindow.SetActive (true);
		mainCanvas.SetActive (false);
		isPaused = true;
		Time.timeScale = 0f;
	}

	public void resume()
	{
		settingsWindow.SetActive (false);
		mainCanvas.SetActive (true);
		isPaused = false;
		Time.timeScale = 1f;
	}
}
