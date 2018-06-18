using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoorLook : MonoBehaviour {
	public int level;
	LevelStat levelInfo;
	public GameObject levelPassedLabel;
	public GameObject allFruitsLabel;
	public GameObject allCrystalsLabel;
	public Sprite allFruitsSprite;
	public Sprite allCrystalSprite;
	// Use this for initialization
	void Awake(){
		levelInfo = LevelStat.ReadStat ("level"+level);
	}
	void Start () {
		setLevelPassedlabel ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider) {
		SceneManager.LoadScene ("Level0"+level);
	}

	void setLevelPassedlabel(){
		if (levelInfo.levelPassed) {
			levelPassedLabel.GetComponent<SpriteRenderer> ().sortingLayerName = "NotBackGround";
			levelPassedLabel.GetComponent<SpriteRenderer> ().sortingOrder = 10;
		}
	}

	void setFruitsCollectedLabel(){
		if (levelInfo.hasAllFruits) {
			allFruitsLabel.GetComponent<SpriteRenderer> ().sprite = allFruitsSprite;
		}
	}

	void setCrystalsCollectedLabel(){
		if (levelInfo.hasCrystals) {
			allCrystalsLabel.GetComponent<SpriteRenderer> ().sprite = allCrystalSprite;
		}
	}
}
