using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	public Vector3 MoveBy;
	public float speed;
	public double WaitingTime;


	Vector3 pointA;
	Vector3 pointB;
	bool going_to_a = false;
	double time_to_wait;
	//Rigidbody2D myBody;
	EdgeCollider2D myCollider;
	// Use this for initialization
	void Start () {
		this.pointA = this.transform.position;
		this.pointB = this.pointA + MoveBy;
		time_to_wait = WaitingTime;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		Vector3 my_pos = this.transform.position;
		Vector3 target;
		if(going_to_a) {
			target = this.pointA;
		} else {
			target = this.pointB;
		}
		Vector3 destination = target - my_pos;
		destination.z = 0;

		if (isArrived (my_pos, target)) {
			time_to_wait -= Time.deltaTime;
			if (time_to_wait <= 0) {
				going_to_a = !going_to_a;
				time_to_wait = WaitingTime;
			}
		} else {
			float moveStep = speed * Time.deltaTime;
			this.transform.position = Vector3.MoveTowards (my_pos, target, moveStep);
		}
			
	}

	bool isArrived(Vector3 pos, Vector3 target) {
		pos.z = 0;
		target.z = 0;
		return Vector3.Distance(pos, target) < 0.02f;
	}
}
