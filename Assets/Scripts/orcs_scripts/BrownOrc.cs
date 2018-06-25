using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownOrc : Orc {
	public GameObject prefabCarrot;
	public float radius;
	float last_carrot_time = 0;
	// Use this for initialization
	/*void Start () {
	}*/


	protected override void OnRabitCollision(HeroRabit rabbit){
		float angle = angleBetweenMeAndRabbit (rabbit);
		if (Mathf.Abs(angle) >= 150 && Mathf.Abs(angle) <= 180 )
			this.DieWithAnimation ();
		//else
		//	HeroRabit.lastRabit.DieWithAnimation ();
	}
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		Vector3 my_pos = this.transform.position;
		Vector3 rabbit_pos = HeroRabit.lastRabit.transform.position;
		float rabitDir = ToRabbitDirection(my_pos,rabbit_pos);
		Vector2 myVelocity = myBody.velocity;
		//Debug.Log ("Rabit direction Brown Orc:" + rabitDir);
		if (rabitDir != 0.0f && Mathf.Abs (rabbit_pos.x - my_pos.x) < radius) {
			myAnimator.SetBool ("walk", false);
			throwCarrotWeapon (my_pos,rabbit_pos,rabitDir);
			myVelocity.x = 0.0f;
			myBody.velocity = myVelocity;
		} else {
			myAnimator.SetBool ("walk", true);
			//Vector2 myVelocity = myBody.velocity;
			//Debug.Log("My direction Brown Orc" + GetDirection (my_pos));
			myVelocity.x = speed * GetDirection (my_pos);
			myBody.velocity = myVelocity;
		}
		SwitchModes ();
		//Debug.Log ("My flipx"+mySpriteRenderer.flipX );
		//Debug.Log ("My mode"+ mode );
	}

	void throwCarrotWeapon(Vector3 my_pos, Vector3 rabbit_pos, float rabbit_dir){
		//Vector3 rabit_pos = HeroRabit.lastRabit.transform.position;
		//fix the time of last launch
		//last_carrot_time = Time.time;
		//check launch time
		//if (Mathf.Abs (rabbit_pos.x - my_pos.x) < radius) {
			if(Time.time - last_carrot_time >= 2.0f) {
			//Launch carrot
				myAnimator.SetTrigger("attack");
				launchCarrot (rabbit_dir);
				last_carrot_time = Time.time;
			}
		//}
	}

	void launchCarrot(float direction) {
		//create Prefub copy
		GameObject obj = GameObject.Instantiate(this.prefabCarrot);
		Vector3 pos =  this.transform.position;
		pos.y += 0.5f;
		obj.transform.position = pos;
		CarrotWeapon carrot = obj.GetComponent<CarrotWeapon> ();
		carrot.launch (direction);
	}
}
