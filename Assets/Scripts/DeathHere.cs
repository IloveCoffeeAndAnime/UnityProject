﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHere : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onTriggerEnter2D(Collider2D collider){
		HeroRabit rabit = collider.GetComponent<HeroRabit> ();
		if (rabit != null) {
			LevelController.current.OnRabitDeath (rabit);
		}
	}
}
