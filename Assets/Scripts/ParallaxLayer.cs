using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour {
	public float slowDown = 0.5f;
	Vector3 lastPosition;

	// Use this for initialization
	void Start () {
		lastPosition = Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate(){
		Vector3 newPosition = Camera.main.transform.position;
		Vector3 diff = newPosition - lastPosition;
		lastPosition = newPosition;
		Vector3 myPos = this.transform.position;
		myPos += slowDown * diff;
		this.transform.position = myPos;
	}
}
