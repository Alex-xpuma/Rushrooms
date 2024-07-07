using UnityEngine;
using System.Collections;

//script for pickups. We increase pickup multiplier and its duration, add score, when player collides with a pickup.
public class Pickup : MonoBehaviour {

	[SerializeField] private float multiplierIncreament;
	[SerializeField] private float multiplierDuration;
	[SerializeField] private float scoreIncrement;
	[SerializeField] private AudioClip sound;

	private ScoreManager scoreManager;
	private AudioSource source;

	void Awake ()
	{
		GameObject gameManager = GameObject.Find ("GameManager");

		scoreManager = gameManager.GetComponent<ScoreManager> ();

		source = gameManager.GetComponent<AudioSource> ();
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			source.PlayOneShot (sound, 0.5f);

			if (multiplierIncreament != 0)
				scoreManager.PickupMultiplier += multiplierIncreament;

			if (multiplierDuration != 0)
				scoreManager.PickupMultiplierDuration += multiplierDuration;

			if (scoreIncrement != 0)
				scoreManager.Score = scoreIncrement;

			gameObject.SetActive (false);
		}
	}
}
