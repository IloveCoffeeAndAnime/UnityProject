using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotWeapon : Collectable {

	float speed = 3;
	//public Vector3 pointA;
	//public Vector3 pointB;
	// Use this for initialization
	void Start () {
		//StartCoroutine (destroyLater ());
	}

	// Update is called once per frame
	void Update () {
		/*Vector3 myPos = this.transform.position;
		if (myPos.x > Mathf.Max (pointA.x, pointB.x) || myPos.x < Mathf.Min (pointA.x, pointB.x))
			Destroy (this.gameObject);*/
	}

	protected override void OnRabitHit (HeroRabit rabit){
		Destroy (this.gameObject);
		rabit.DieWithAnimation();
	}

	IEnumerator destroyLater() {
		yield return new WaitForSeconds (2.0f);
		Destroy (this.gameObject);
	}

	IEnumerator waitForThrowAniation(){
		yield return new WaitForSeconds (0.5f);
	}
	public void launch(float direction){
		StartCoroutine (waitForThrowAniation());
		Rigidbody2D myRigidBody = this.GetComponent<Rigidbody2D>();
		Vector2 vel = myRigidBody.velocity;
		vel.x = speed * direction;
		myRigidBody.velocity = vel;
	}
}
