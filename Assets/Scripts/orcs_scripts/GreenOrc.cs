using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrc : Orc {
	/*public enum Mode {
		GoToA,
		GoToB,
		Attack
	}
	public Vector3 pointA;
	public Vector3 pointB;
	public float speed;*/

	/*SpriteRenderer mySpriteRenderer;
	Mode mode = Mode.GoToA;
	float epsilon = 0.1f;
	Rigidbody2D myBody;
	Animator myAnimator;
	Vector3 myStartingPos;*/

	//public Vector3 StartPosition{ get{return myStartingPos; } private set{ myStartingPos = value;}}

	// Use this for initialization
	//void Start () {
		/*myStartingPos = this.transform.position;
		mySpriteRenderer = this.GetComponent<SpriteRenderer> ();
		myBody = this.GetComponent<Rigidbody2D> ();
		myAnimator = this.GetComponent<Animator> ();*/
	//}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		Vector3 my_pos = this.transform.position;
		Vector3 rabbit_pos = HeroRabit.lastRabit.transform.position;
		float rabitDir = ToRabbitDirection(my_pos,rabbit_pos);
		Vector2 myVelocity = myBody.velocity;
		//Debug.Log ("Rabit direction:" + rabitDir);
		if (rabitDir != 0f) {
			myAnimator.SetBool ("run", true);
			myVelocity.x = 2 * speed * rabitDir;
		} else { 
			myAnimator.SetBool ("run", false);
			myAnimator.SetBool ("walk", true);
			myVelocity.x = speed * GetDirection (my_pos);
		}
		myBody.velocity = myVelocity;
		SwitchModes ();

	}

	protected override void OnRabitCollision(HeroRabit rabbit){
		myAnimator.SetTrigger ("attack");
		float angle = angleBetweenMeAndRabbit (rabbit);
		Debug.Log ("Angle:"+ angle);
		if (Mathf.Abs(angle) >= 150 && Mathf.Abs(angle) <= 180 )
			this.DieWithAnimation ();
		else
			HeroRabit.lastRabit.DieWithAnimation ();
		
	}

	/*void OnCollisionEnter2D(Collision2D collider){
		myAnimator.SetTrigger ("attack");
		float angle = angleBetweenMeAndRabbit (HeroRabit.lastRabit);
		Debug.Log ("Angle:"+ angle);
		if (angle > 110 && angle < 125 )
			this.DieWithAnimation ();
		else
			HeroRabit.lastRabit.DieWithAnimation ();

	}*/

	/* void SwitchModes(){
		if(mode == Mode.GoToA) {
			if(IsArrived(pointA)) {
				mySpriteRenderer.flipX = true;
				mode = Mode.GoToB;
				Debug.Log ("Arrived to A");
			}
		} else if(mode == Mode.GoToB) {
			if(IsArrived(pointB)) {
				mySpriteRenderer.flipX = false;
				mode = Mode.GoToA;
				Debug.Log ("Arrived to B");
			}
		}
	}
		
	bool IsArrived(Vector3 targetPoint){
		float diff = this.transform.position.x - targetPoint.x;
		//Debug.Log (diff.magnitude);
		return  Mathf.Abs(diff) <=epsilon;
	}

	float GetDirection(Vector3 my_pos){
		//Vector3 my_pos = this.transform.position;
		if (mode == Mode.GoToA) {
			//Direction depending on target
			if (my_pos.x < pointA.x) {
				return 1;
			} else {
				return -1;
			}
		} else if (mode == Mode.GoToB) {
			if (my_pos.x > pointB.x)
				return -1;
			else
				return 1;
		}
		return 0;
	}

	float ToRabbitDirection(Vector3 my_pos){
		//Vector3 my_pos = this.transform.position;
		Vector3 rabit_pos = HeroRabit.lastRabit.transform.position;
		if (rabit_pos.x > Mathf.Min (pointA.x, pointB.x)
			&& rabit_pos.x < Mathf.Max (pointA.x, pointB.x))
		{
			mode = Mode.Attack;
		}
		if(mode == Mode.Attack) {
			//Move towards rabit
			if(my_pos.x < rabit_pos.x) {
				mySpriteRenderer.flipX = true;
				return 1;
			} else {
				mySpriteRenderer.flipX = false;
				return -1;
			}
		}
		return 0;
	}

	float angleBetweenMeAndRabbit(HeroRabit rabbit){
		Vector3 dir = this.transform.position - rabbit.transform.position;
		return Mathf.Atan2 (dir.x, dir.y) * Mathf.Rad2Deg;
	}

	void DieWithAnimation(){
		StartCoroutine (WaitForDeathAnimationAndDisappear());
	}



	IEnumerator WaitForDeathAnimationAndDisappear(){
		myAnimator.SetTrigger ("death");
		yield return new WaitForSeconds(myAnimator.GetCurrentAnimatorStateInfo(0).length);
		Destroy(this.gameObject);
	}*/

}
