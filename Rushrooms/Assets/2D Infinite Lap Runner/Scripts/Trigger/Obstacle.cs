using UnityEngine;
using System.Collections;

//script for obstacles
public class Obstacle : MonoBehaviour {

	private ScoreManager scoreManager;

	void Awake ()
	{
		scoreManager = GameObject.Find ("GameManager").GetComponent<ScoreManager> ();
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") //if player collide with a obstacle, we reset the pickup multiplier and its duration to 0
		{
				scoreManager.PickupMultiplier = 0f;
			
				scoreManager.PickupMultiplierDuration = 0;
		}
	}
}
