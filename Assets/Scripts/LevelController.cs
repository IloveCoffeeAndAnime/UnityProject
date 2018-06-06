using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
	public static LevelController current;
	public GameObject lifePanel;
	public GameObject coinsPanel;
	public GameObject crystalPanel;
	static int lifeCount = 3;
	static int coinsCount = 0;
	static int crystalCount  = 0;

	public Sprite[] crystalSpriteArr;

	Vector3 startingPosition;

	void Awake()
	{
		current = this;
	}

	public void OnRabitDeath (HeroRabit rabit){
		rabit.transform.position = this.startingPosition;
		SpriteRenderer renderer = rabit.GetComponent<SpriteRenderer> ();
		renderer.flipX = false;
		removeLife ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setStartPosition (Vector3 pos){
		this.startingPosition = pos;
	}

	public void addCoins (int coins){
		if (coinsPanel != null) {
			coinsCount += coins;
			coinsPanel.GetComponentInChildren<Text> ().text = coinsCount.ToString().PadLeft(4,'0') ;
		}
	}

	public void addCrystal(Crystal.CrystalType crystalType){
		if (crystalPanel != null) {
			crystalPanel.GetComponentsInChildren<SpriteRenderer> () [crystalCount++].sprite =crystalSpriteArr [(int)crystalType];
		}
	}
		
	public void removeLife(){
		if (lifePanel != null) {
			lifeCount--;
			if(lifeCount <=0)
				SceneManager.LoadScene ("ChoseLevel");
			SpriteRenderer[] childSprites = lifePanel.GetComponentsInChildren<SpriteRenderer> ();
			childSprites [lifeCount * 2].sortingOrder = 2;
			childSprites [lifeCount * 2+1].sortingOrder = 1;
		}
	}
}
