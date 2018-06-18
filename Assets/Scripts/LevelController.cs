using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {
	public static LevelController current;
	public int levelNumber;
	public GameObject lifePanel;
	public GameObject coinsPanel;
	public GameObject crystalPanel;
	public GameObject fruitPanel;
	public const int fruitsOnLevel = 11;
	public const int livesOnLevel = 3;
	public const int crystalsOnLevel = 3;
	int lifeCount = 3;
	int coinsCount = 0;
	int crystalCount  = 0;
	int fruitCount = 0;
	LevelStat levelSaver;
	List<Crystal.CrystalType> collectedCrystals= new List<Crystal.CrystalType>();

	public AudioClip deathSound = null;
	AudioSource deathSource = null;

	public Sprite[] crystalSpriteArr;

	Vector3 startingPosition;


	public int getCoinsCount(){
		return coinsCount;
	}
	public int getFruitsCount(){
		return fruitCount;
	}
	public Crystal.CrystalType[] CollectedCrystals(){
		return collectedCrystals.ToArray();
	}

	public void setLevelPassed(bool val){
		this.levelSaver.levelPassed = val;
	}
	public void setAllFruitsCollected(bool val){
		this.levelSaver.hasAllFruits = val;
	}
	public void setAllCrystalsCollected(bool val){
		this.levelSaver.hasCrystals = val;
	}

	void Awake()
	{
		current = this;
		levelSaver = LevelStat.ReadStat ("level" + levelNumber);
	}

	// Use this for initialization
	void Start () {
		deathSource = this.gameObject.AddComponent<AudioSource> ();
		deathSource.clip = deathSound;
		if(fruitPanel !=null) showAlreadyCollectedFruit ();
	}

	// Update is called once per frame
	void Update () {
	}

	public void OnRabitDeath (HeroRabit rabit){
		if (SoundManager.Instance.isSoundOn ())
			deathSource.Play ();
		rabit.transform.position = this.startingPosition;
		SpriteRenderer renderer = rabit.GetComponent<SpriteRenderer> ();
		renderer.flipX = false;
		removeLife ();
	}
		
	public void RestartLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		showAlreadyCollectedFruit ();
		/*HeroRabit.lastRabit.transform.position = this.startingPosition;
		SpriteRenderer renderer = HeroRabit.lastRabit.GetComponent<SpriteRenderer> ();
		renderer.flipX = false;
		addCoins (-coinsCount);
		addFruit (-fruitCount);
		reloadLives ();
		removeAllCollectedCrystals ();
		//lives should be added completely
		//crystals should be removed*/

		//SceneManager.GetActiveScene ().name;
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
		if (crystalPanel != null ) {
			if (crystalCount < crystalsOnLevel) {
				crystalPanel.GetComponentsInChildren<SpriteRenderer> () [crystalCount++].sprite = crystalSpriteArr [(int)crystalType];
				collectedCrystals.Add (crystalType);
			}
		}
	}
/*	public void removeAllCollectedCrystals(){
		collectedCrystals.Clear ();
		crystalCount = 0;
		SpriteRenderer[] crystals = crystalPanel.GetComponentsInChildren<SpriteRenderer> ();
		for (int i = 0; i < crystalsOnLevel; i++) {
			crystals[i].sprite = crystalSpriteArr[(int)Crystal.CrystalType.Empty];
		}
	}*/

	public void addFruit(int fruits){
		if (fruitPanel != null) {
			fruitCount+=fruits;
			fruitPanel.GetComponentInChildren<Text> ().text = fruitCount + " / " + fruitsOnLevel;
		}
	}

/*	public void reloadLives(){
		lifeCount = livesOnLevel;
		SpriteRenderer[] childSprites = lifePanel.GetComponentsInChildren<SpriteRenderer> ();
		for (int i = 0; i < livesOnLevel; i++) {
			childSprites [i * 2].sortingOrder = 1;
			childSprites [i * 2+1].sortingOrder = 2;
		}
	}*/

	public void removeLife(){
		if (lifePanel != null) {
			lifeCount--;
			if (lifeCount <= 0)
				WindowsController.current.initLostLevelWindow ();
				//SceneManager.LoadScene ("ChoseLevel");
			SpriteRenderer[] childSprites = lifePanel.GetComponentsInChildren<SpriteRenderer> ();
			childSprites [lifeCount * 2].sortingOrder = 2;
			childSprites [lifeCount * 2+1].sortingOrder = 1;
		}
	}
	public void addLife()
	{
		if (lifePanel != null) {
			if (lifeCount < livesOnLevel) {
				SpriteRenderer[] childSprites = lifePanel.GetComponentsInChildren<SpriteRenderer> ();
				childSprites [lifeCount * 2].sortingOrder = 1;
				childSprites [lifeCount * 2+1].sortingOrder = 2;
				lifeCount++;
			}
		}
	}

	public void AddFruitToCollected(GameObject fruit){
		levelSaver.AddFruitToCollected(fruit);
	}

	public void saveLevel(){
		if (levelSaver != null) {
			setLevelPassed (true);
			if (fruitCount == fruitsOnLevel)
				setAllFruitsCollected (true);
			if (crystalCount == crystalsOnLevel)
				setAllCrystalsCollected (true);
			LevelStat.SaveStat (levelSaver, "level" + levelNumber);
			int allCoins = LevelStat.GetCoinsCount ();
			allCoins += coinsCount;
			LevelStat.SaveCoinsCount (allCoins);
		}
	}
		
	private void showAlreadyCollectedFruit(){
		if (levelSaver != null) {
			GameObject[] fruitsOnLevel = GameObject.FindGameObjectsWithTag ("Fruit");
			GameObject currFruit;
			for (int i = 0; i < fruitsOnLevel.Length; i++) {
				currFruit = fruitsOnLevel [i];
				if (levelSaver.isFruitCollected (currFruit))
					currFruit.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
			}
			addFruit (levelSaver.collectedFruitCount ());
		}
	}
}
