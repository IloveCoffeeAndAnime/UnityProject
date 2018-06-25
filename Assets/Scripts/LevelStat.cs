
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelStat{
	public bool hasCrystals = false;
	public bool hasAllFruits = false;
	public bool levelPassed = false;
	//public List<int> collectedFruits = new List<int> ();
	public List<Vector3> collectedFruitsPos = new List<Vector3> ();

	public static void SaveStat(LevelStat stats, string name){
		string str = JsonUtility.ToJson (stats);
		PlayerPrefs.SetString (name, str);
	}

	public static LevelStat ReadStat(string name){
		//PlayerPrefs.DeleteAll ();
		string str = PlayerPrefs.GetString (name, null);
		LevelStat stats = JsonUtility.FromJson<LevelStat> (str);
		if (stats==null) {
			stats = new LevelStat ();
		}
		return stats;
	}
		
	public static void SaveCoinsCount(int coinsNumber){
		PlayerPrefs.SetInt ("coins", coinsNumber);
		PlayerPrefs.Save ();
	}
	public static int GetCoinsCount(){
		int coins = PlayerPrefs.GetInt ("coins", 0);
		return coins;
	}

	public void AddFruitToCollected(GameObject fruit){
		collectedFruitsPos.Add (fruit.GetComponent<Transform>().position);
	}
	public bool isFruitCollected(GameObject fruit){
		return collectedFruitsPos.Contains (fruit.GetComponent<Transform>().position);
	}

	/*public Vector3[]  GetCollectedFruits(){
		Vector3[] fruitList;
		collectedFruitsPos.CopyTo (fruitList);
		return fruitList;
	}*/
	public int collectedFruitCount(){
		return collectedFruitsPos.Count;
	}
}
