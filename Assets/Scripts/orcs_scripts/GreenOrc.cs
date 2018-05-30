using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrc : MonoBehaviour {
	public enum Mode {
		GoToA,
		GoToB,
		Attack
	}
	public Vector3 pointA;
	public Vector3 pointB;
	public float speed;

	Mode mode = Mode.GoToA;
	Vector3 rabit_pos;

	// Use this for initialization
	void Start () {
		rabit_pos = HeroRabit.lastRabit.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	 void SwitchModes(){
		if(mode == Mode.GoToA) {
			if(IsArrived(pointA)) {
				mode = Mode.GoToB;
			}
		} else if(mode == Mode.GoToB) {
			if(IsArrived(pointB)) {
				mode = Mode.GoToA;
			}
		}
	}

	void FixedUpdate(){
		float moveStep = speed * Time.deltaTime;
		Vector3 my_pos = this.transform.position;
		SwitchModes ();
		if(mode == Mode.GoToA)
			this.transform.position = Vector3.MoveTowards (my_pos, pointA, moveStep);
		else if(mode == Mode.GoToB)
			this.transform.position = Vector3.MoveTowards (my_pos, pointB, moveStep);
			
	}
		
	bool IsArrived(Vector3 targetPoint){
		return  this.transform.position == targetPoint;//???????????????????
	}

	float GetDirection(){
		Vector3 my_pos = this.transform.position;
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

	float ToRabbitDirection(){
		Vector3 my_pos = this.transform.position;
		if (rabit_pos.x > Mathf.Min (pointA.x, pointB.x)
			&& rabit_pos.x < Mathf.Max (pointA.x, pointB.x))
		{
			mode = Mode.Attack;
		}
		if(mode == Mode.Attack) {
			//Move towards rabit
			if(my_pos.x < rabit_pos.x) {
				return 1;
			} else {
				return -1;
			}
		}
		return 0;
	}
}
