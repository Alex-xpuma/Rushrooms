using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	[SerializeField] private GameObject player;
	[SerializeField] private Text scoreText;
	[SerializeField] private Text lapText;
	[SerializeField] private Text multiplierText;
	[SerializeField] private Text pickupMultiplierDurationText;
	[SerializeField] private Text gameOverScoreText;


	private int lapCount;
	private float baseMultiplier = 1f; //the permanent part of the multiplier, increase 0.1 each lap
	private float pickupMultiplier = 0f; //the temporary part, affects by pickups
	private float score = 0;
	private float pickupMultiplierDuration = 0f; //how much time is the temporary multiplier effect left
	private Transform playerTransform;
	private Rigidbody2D playerRigidbody2D;
	
	public float BaseMultiplier
	{
		get
		{
			return baseMultiplier;
		}
		set
		{
			baseMultiplier = value;

			UpdateMultiplierText (); //update multiplier text, everytime baseMultiplier changes
		}
	}

	public float PickupMultiplier
	{
		get
		{
			return pickupMultiplier;
		}
		set
		{
			pickupMultiplier = value;

			UpdateMultiplierText (); //update multiplier text, everytime pickupMultiplier changes
		}
	}

	public float PickupMultiplierDuration
	{	
		get
		{
			return pickupMultiplierDuration;
		}
		set
		{
			pickupMultiplierDuration = value;

			if (pickupMultiplierDuration <= 0f)
				pickupMultiplierDurationText.text = ""; //if pickupMultiplierDuration is or less than 0, we leave the text empty
			else
				pickupMultiplierDurationText.text = pickupMultiplierDuration.ToString("F2") + " s"; //if it's not greater than 0, we show how much time the effect is left.
		}
	}

	//the total multiplier, permanent + temporary.
	public float Multiplier
	{
		get
		{
			return baseMultiplier + pickupMultiplier;
		}
	}

	//update the score
	public float Score
	{
		set
		{
			score += value * Multiplier;
		}
	}

	void Start ()
	{
		lapText.text = "0 Laps";
		scoreText.text = "Score: 0";
		multiplierText.text = "";
		pickupMultiplierDurationText.text = "";

		playerTransform = player.transform;

		playerRigidbody2D = player.GetComponent<Rigidbody2D>();
	}
	
	void Update ()
	{
		Score = playerTransform.InverseTransformDirection (playerRigidbody2D.velocity).x * Time.deltaTime; //We estimate how long does the player run in each frame, and increase the score.

		if (PickupMultiplierDuration < 0f) //if the time of temporary multiplier runs out, we set it to 0.
		{
			PickupMultiplierDuration = PickupMultiplier = 0f; 
		}
		else if (PickupMultiplierDuration > 0) //otherwise, we reduce the time
			PickupMultiplierDuration -= Time.deltaTime; 

		scoreText.text = "Score: " + score.ToString("F0"); //update the socre text
	}

	void UpdateMultiplierText ()
	{
		if (Multiplier == 1f) //if the multiplier is 1, we hide the text
			multiplierText.text = "";
		else //otherwise, we show what the multiplier is.
			multiplierText.text = "x" + Multiplier.ToString ("F1");
	}

	void IncreaseLapCountByOne () //everytime player finishes one lap, we call this method
	{
		lapCount += 1; //increase lap count by 1

		BaseMultiplier += 0.1f; //permanently increase multiplier by 0.1 each lap


		//deal with the plural and singular form of 'Lap'
		if (lapCount == 1)
			lapText.text = "1 Lap";
		else
			lapText.text = lapCount.ToString () + " Laps";
	}

	void GameOverScore () //method called when game is over
	{
		int currentScore = (int)score;

		if (currentScore > PlayerPrefs.GetInt ("HighScore", 0)) //see if the score of current run is higher than the record 
		{
			gameOverScoreText.text = "New Socre Record: " + currentScore.ToString (); //show score text with congrats

			PlayerPrefs.SetInt ("HighScore", currentScore); //we store the current score as the new high score
		}
		else
			gameOverScoreText.text = "Your Score: " + currentScore.ToString (); //if it's lower than record, just show socre text
	}
}
