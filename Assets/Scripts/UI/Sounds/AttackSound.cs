using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSound : MonoBehaviour {
	public AudioClip attackSound = null;
	AudioSource attackSource = null;
	// Use this for initialization
	void Start () {
		attackSource = gameObject.AddComponent<AudioSource> ();
		attackSource.clip = attackSound;
	}
	
	// Update is called once per frame
	void Update () {
	}
	void onAttack() {
		if(SoundManager.Instance.isSoundOn()) {
			attackSource.Play();
		}
	}
}
