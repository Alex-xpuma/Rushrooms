using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//this script does the countdown
public class StartGame : MonoBehaviour {

	[SerializeField] private Animator HUD;
	[SerializeField] private Text countdownText;
	[SerializeField] private AudioClip go;
	[SerializeField] private AudioClip beep;
	[SerializeField] private GameObject player;

	void Start ()
	{
		countdownText.text = "";

		StartCoroutine (Countdown (3)); //we start the countdown from 3
	}
	
	IEnumerator Countdown (int t)
	{
		AudioSource audio = GetComponent<AudioSource> ();

		for (int i = 0; i < t; i++)
		{
			countdownText.text = (t - i).ToString (); //update the countdown number
			
			audio.PlayOneShot (beep, 1f); //play the countdown sound

			yield return new WaitForSeconds(1f); //wait 1 second before doing the next loop
		}
		
		countdownText.text = "Go!"; //display 'Go!' when game starts
		
		audio.PlayOneShot (go, 1f); //play the sound
		
		HUD.SetTrigger ("StartGame"); //play the StartGameHUD animation. Don't forget to check what this animation does.
		
		player.gameObject.SetActive (true); //active the player object
	}
}
