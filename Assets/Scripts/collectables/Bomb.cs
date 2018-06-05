using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void OnRabitHit(HeroRabit rabit) {
		if (!rabit.SuperRabit) {
			this.CollectedHide ();
			if (rabit.IsBig) {
				rabit.BecomeSmaller ();
				rabit.SuperRabit = true;
			} else {
				rabit.DieWithAnimation();
			}
		}
	}


}
