using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable {
	public enum CrystalType{
		Blue=0,
		Green,
		Red
	}

	public CrystalType type;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnRabitHit(HeroRabit rabit) {
		LevelController.current.addCrystal (type);
		this.CollectedHide ();
	}
}
