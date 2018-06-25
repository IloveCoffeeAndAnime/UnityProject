using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnRabitHit(HeroRabit rabit) {
		this.CollectedHide ();
		//Debug.Log (this.gameObject);
		if (!LevelController.current.IsFruitCollected (this.gameObject)) {
			LevelController.current.addFruit (1);
			LevelController.current.AddFruitToCollected (this.gameObject);
		}
	}
}
