using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {
	public AudioClip music = null;
	AudioSource musicSource = null;
	private bool musicOn;
	public Sprite musicOnSprite;
	public Sprite musicOffSprite;
	public Button musicBtn;

	public bool IsMusicOn(){
		return this.musicOn;
	}

	void Awake(){
		this.musicOn = PlayerPrefs.GetInt ("music", 1) == 1;
	}

	// Use this for initialization
	void Start () {
		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.clip = music;
		musicBtn.onClick.AddListener(onMusicBtnClicked);
		musicSource.loop = true;
		if (IsMusicOn ()) {
			setMusicOn ();
		} else {
			setMusicOff ();
		}
	}

	public void setMusicOn() {
		this.musicOn = true;
		musicSource.Play ();
		PlayerPrefs.SetInt ("music", 1);
		PlayerPrefs.Save ();
		musicBtn.image.sprite = musicOnSprite; 
	}

	public void setMusicOff(){
		this.musicOn = false;
		musicSource.Stop ();
		PlayerPrefs.SetInt ("music",0);
		PlayerPrefs.Save ();
		musicBtn.image.sprite = musicOffSprite;
	}

	public void onMusicBtnClicked(){
		if (IsMusicOn ()) {
			setMusicOff();
		} else {
			setMusicOn();
		}
	}

}
