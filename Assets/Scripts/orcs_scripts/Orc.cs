using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Orc : MonoBehaviour {
	public enum Mode {
		GoToA,
		GoToB,
		Attack
	}
	public Vector3 pointA;
	public Vector3 pointB;
	public float speed;

	float epsilon = 0.1f;
	protected SpriteRenderer mySpriteRenderer;
	protected Mode mode = Mode.GoToA;
	protected Rigidbody2D myBody;
	protected Animator myAnimator;
	Vector3 myStartingPos;

	public Vector3 StartPosition{ get{return myStartingPos; } protected set{ myStartingPos = value;}}

	protected virtual void OnRabitCollision(HeroRabit rabbit){
	}

	// Use this for initialization
	void Start () {
		myStartingPos = this.transform.position;
		mySpriteRenderer = this.GetComponent<SpriteRenderer> ();
		myBody = this.GetComponent<Rigidbody2D> ();
		//Debug.Log (myBody);
		myAnimator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision){
		OnRabitCollision (HeroRabit.lastRabit);
		
	}

	protected void SwitchModes(){
		Vector3 rabit_pos = HeroRabit.lastRabit.transform.position;
		Vector3 my_pos = this.transform.position;
		if (rabit_pos.x > Mathf.Min (pointA.x, pointB.x)
			&& rabit_pos.x < Mathf.Max (pointA.x, pointB.x)) {
			mode = Mode.Attack;
		} else {
			mode = mySpriteRenderer.flipX ? Mode.GoToB : Mode.GoToA;
		}
		if(mode == Mode.GoToA) {
			if(IsArrived(pointA)) {
				mySpriteRenderer.flipX = true;
				mode = Mode.GoToB;
				//Debug.Log ("Arrived to A");
			}
		} else if(mode == Mode.GoToB) {
			if(IsArrived(pointB)) {
				mySpriteRenderer.flipX = false;
				mode = Mode.GoToA;
				//Debug.Log ("Arrived to B");
			}
		}
	}

	protected bool IsArrived(Vector3 targetPoint){
		float diff = this.transform.position.x - targetPoint.x;
		return  Mathf.Abs(diff) <=epsilon;
	}

	protected float GetDirection(Vector3 my_pos){
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

	protected float ToRabbitDirection(Vector3 my_pos,Vector3 rabit_pos){
		if(mode == Mode.Attack) {
			//Move towards rabit
			if(my_pos.x < rabit_pos.x) {
				mySpriteRenderer.flipX = true;
				return 1;
			} else {
				mySpriteRenderer.flipX = false;
			//	Debug.Log("I am flipx false");
				return -1;
			}
		}
		return 0;
	}

	protected float angleBetweenMeAndRabbit(HeroRabit rabbit){
		Vector3 dir = this.transform.position - rabbit.transform.position;
		return Mathf.Atan2 (dir.x, dir.y) * Mathf.Rad2Deg;
	}

	protected void DieWithAnimation(){
		StartCoroutine (WaitForDeathAnimationAndDisappear());
	}

	IEnumerator WaitForDeathAnimationAndDisappear(){
		myAnimator.SetTrigger ("death");
		yield return new WaitForSeconds(myAnimator.GetCurrentAnimatorStateInfo(0).length);
		Destroy(this.gameObject);
	}
}
