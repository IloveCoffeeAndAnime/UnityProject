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
		this.CollectedHide ();
		//Animator animator = rabit.GetComponent<Animator> ();
		//animator.SetTrigger("reset");
		rabit.DieWithAnimation();
		//animator.SetTrigger("reset");
		//if (animator.GetCurrentAnimatorStateInfo (0).IsName ("DeathAnimation")) {
			//LevelController.current.OnRabitDeath (rabit);
		//}
		//animator.SetBool ("death", true);
	}


}
