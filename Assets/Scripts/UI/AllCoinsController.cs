using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllCoinsController : MonoBehaviour {

	public GameObject allCoinsPanel;
	// Use this for initialization
	void Start () {
		int coinsCount = LevelStat.GetCoinsCount ();
		allCoinsPanel.GetComponentInChildren<Text>().text = coinsCount.ToString().PadLeft(4,'0') ;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
