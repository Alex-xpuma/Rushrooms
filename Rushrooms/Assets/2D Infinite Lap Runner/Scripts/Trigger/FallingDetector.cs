using UnityEngine;
using System.Collections;

//when player fall into a hole, the game will be over
public class FallingDetector : MonoBehaviour {

	private GameManager gameManager;

	void Start ()
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") //call the GameOver method when game is over
			gameManager.SendMessage ("GameOver", SendMessageOptions.DontRequireReceiver);
	}
}
