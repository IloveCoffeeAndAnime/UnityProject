using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
	public static LevelController current;

	Vector3 startingPosition;

	void Awake()
	{
		current = this;
	}

	public void OnRabitDeath (HeroRabit rabit){
		rabit.transform.position = this.startingPosition;
		SpriteRenderer renderer = rabit.GetComponent<SpriteRenderer> ();
		renderer.flipX = false;
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
		
	}
}
