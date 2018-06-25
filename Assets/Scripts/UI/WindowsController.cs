using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WindowsController : MonoBehaviour {
	public static WindowsController current;
	public GameObject winWindow;
	public GameObject lostLevelWindow;
	public GameObject mainCanvas;
	public AudioClip winSound = null;
	AudioSource winSource = null;
	public AudioClip lostLevelSound = null;
	AudioSource lostLevelSource = null;
	// Use this for initialization

	void Awake(){
		current = this;
	}

	void Start () {
		winWindow.SetActive (false);
		lostLevelWindow.SetActive (false);
		mainCanvas.SetActive (true);
		winSource = this.gameObject.AddComponent<AudioSource> ();
		winSource.clip = winSound;
		lostLevelSource = this.gameObject.AddComponent<AudioSource> ();
		lostLevelSource.clip = lostLevelSound;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GoToMainMenu(){
		SceneManager.LoadScene ("ChoseLevel");
	}
	public void SwitchCanvases(GameObject canvasOff,GameObject canvasOn){
		canvasOff.SetActive (false);
		canvasOn.SetActive (true);
	}

	public void forRestartBtn(GameObject currWindow)
	{
		SwitchCanvases (currWindow,mainCanvas);
		LevelController.current.RestartLevel ();
	}

	void OnRestartBtnLostLevel(){
		forRestartBtn (lostLevelWindow);
	}

	public void InitWindowBtns(GameObject window){
		GameObject[] toMainMenuBtns = GameObject.FindGameObjectsWithTag ("GoToChooseLevelBtn");
		for (int i = 0; i < toMainMenuBtns.Length; i++) {
			toMainMenuBtns [i].GetComponent<Button> ().onClick.AddListener (GoToMainMenu);
		}
		GameObject.FindGameObjectWithTag ("RestartLevelBtn").GetComponent<Button> ().onClick.AddListener (delegate() {
			forRestartBtn(window);
		});
	}

	public void InitCrystalInf(){
		Crystal.CrystalType[] collectedCrystals = LevelController.current.CollectedCrystals ();
		GameObject[] crystalsOnRedCloth = GameObject.FindGameObjectsWithTag ("CrystalOnWinWindow");
		for (int i = 0; i < collectedCrystals.Length; i++) {
			crystalsOnRedCloth[i].GetComponent<Image>().sprite = LevelController.current.crystalSpriteArr [(int)collectedCrystals[i]];
		}
	}
		
	public void InitFruitAndCoinsInf(){
		GameObject.FindGameObjectWithTag ("FruitCounterText").GetComponent<Text>().text =LevelController.current.getFruitsCount ()+ "/"+LevelController.current.fruitsOnLevel;
		GameObject.FindGameObjectWithTag ("CoinsCounterText").GetComponent<Text>().text ="+"+ LevelController.current.getCoinsCount ();
	}
	public void initLostLevelWindow(){
		SoundManager.Instance.PlaySoundIfOn (lostLevelSource);
		SwitchCanvases (mainCanvas, lostLevelWindow);
		InitCrystalInf ();
		InitWindowBtns (lostLevelWindow);
	}

	public void initWinWindow(){
		SoundManager.Instance.PlaySoundIfOn (winSource);
		SwitchCanvases (mainCanvas, winWindow);
		LevelController.current.saveLevel ();
		InitCrystalInf ();
		InitFruitAndCoinsInf ();
		InitWindowBtns (winWindow);
	}

}
