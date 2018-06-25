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
	public GameObject levelLock = null;
	public Sprite allFruitsSprite;
	public Sprite allCrystalSprite;
	bool isLocked = false;

	public bool IsLocked(){
		return isLocked;
	}
	// Use this for initialization
	void Awake(){
		levelInfo = LevelStat.ReadStat ("level"+level);
		if (level - 1 > 0) {
			isLocked = ! LevelStat.ReadStat ("level"+(level-1)).levelPassed;
			Debug.Log ("prev elvel passed: "+ LevelStat.ReadStat ("level"+(level-1)).levelPassed);
			Debug.Log ("isLocked inside awake"+isLocked);
		}
	}

	void Start () {
		setLevelPassedlabel ();
		setFruitsCollectedLabel ();
		setCrystalsCollectedLabel ();
		ifOpenedDeleteLock ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collider) {
		SceneManager.LoadScene ("Level0"+level);
	}

	void setLevelPassedlabel(){
		if (levelInfo.levelPassed) {
			levelPassedLabel.GetComponent<SpriteRenderer> ().sortingLayerName = "NotBackground";
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
	void ifOpenedDeleteLock(){
		Debug.Log ("inside ifOpenedDeleteLock: "+IsLocked ()+" on levrel"+level);
		if (!IsLocked () && levelLock != null) {
			levelLock.GetComponent<SpriteRenderer> ().sortingLayerName = "Default";
			levelLock.GetComponent<SpriteRenderer> ().sortingOrder = 0;
		}
	}

}
