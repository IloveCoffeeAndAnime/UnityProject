using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundConroller : MonoBehaviour {
	public Button soundBtn;
	public Sprite soundOnSprite;
	public Sprite soundOffSprite;
	// Use this for initialization
	void Start () {
		if (SoundManager.Instance.isSoundOn ()) {
			soundBtn.image.sprite = soundOnSprite;
		} else {
			soundBtn.image.sprite = soundOffSprite;
		}
		soundBtn.onClick.AddListener (onSoundBtnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onSoundBtnClick(){
		if (SoundManager.Instance.isSoundOn ()) {
			soundBtn.image.sprite = soundOffSprite;
			SoundManager.Instance.setSoundOn (false);
		} else {
			soundBtn.image.sprite = soundOnSprite;
			SoundManager.Instance.setSoundOn (true);
		}
	}

}
