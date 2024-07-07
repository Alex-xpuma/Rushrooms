using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScore : MonoBehaviour {

	void Start ()
	{
		GetComponent<Text> ().text ="Highest Score: " + PlayerPrefs.GetInt ("HighScore", 0); //get the record high score, and display it.
	}
}
