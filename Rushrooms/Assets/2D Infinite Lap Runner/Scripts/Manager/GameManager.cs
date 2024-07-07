using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (AudioSource))] //make sure AudioSource is attached
public class GameManager : MonoBehaviour {

	public float groundRadius; //radius of ground. This controls how larage a lap is.
	[HideInInspector] public Vector2 playerGroundPosition; //the position if player is on ground
	[HideInInspector] public Quaternion playerGroundRotation; //the rotation if player is on ground


	[SerializeField] private Transform player;
	[SerializeField] private Animator HUD;
	[SerializeField] private AudioClip buttonSound;

	//We calculate some values which will be needed by other scripts' Start method, so we do this in the Awake method.
	void Awake ()
	{
		PrepareAtAwake ();
	}

	void Update ()
	{
		playerGroundPosition = GroundPosition (player); //As player will be moving in the game, we need to keep updating the position.
	}

	void PrepareAtAwake ()
	{
		playerGroundPosition = GroundPosition (player);

		playerGroundRotation = Trigonometricf.GetRotation (playerGroundPosition); //Calculating the rotation at a given position
	}

	Vector2 GroundPosition (Transform obj)
	{
		return obj.position.normalized * groundRadius;
	}

	/*Get called when player is dead
	 *The game is over but we still want the background keep running,
	 *so we put the dead player to its normal position and make a invisable fake player,
	 *the fake player will continue running in the scene, as all our dynamic objects are generated based on player's position.
	 *If we simplely disable or destroy the player object, we have to stop generating dynamic environments.
	 */
	void GameOver ()
	{
		player.GetComponent<SpriteRenderer> ().enabled = false; //make the player invisable

		Destroy (player.GetComponent<Rigidbody2D>());

		player.position = player.TransformPoint (new Vector3 (0f, 6f, 0f)); //get player back to the normal position on the platform. 6 happens to be the figure here, change it to serve your scene.

		player.GetComponent<BoxCollider2D> ().enabled = false;

		player.GetComponentInChildren<CircleCollider2D> ().enabled = false;

		player.GetComponent<Animator> ().enabled = false;

		player.GetComponent<PlayerController> ().enabled = false;

		player.GetComponent<FakePlayerMover> ().enabled = true;

		ScoreManager scoreManager = GetComponent<ScoreManager> ();

		scoreManager.SendMessage ("GameOverScore", SendMessageOptions.DontRequireReceiver);

		scoreManager.enabled = false;

		HUD.SetTrigger("GameOver"); //play GameOverHUD animation. This animation does a lot of things including disable certain UI text. You are highly recommended to check the animation out.
	}

	//button onclick event
	//play a sound when click buttons
	public void PlayButtonSound ()
	{
		GetComponent<AudioSource>().PlayOneShot (buttonSound, 0.5f);
	}

	//button onclick event
	//load scene
	public void LoadLevel (int n)
	{
		Application.LoadLevel (n);
	}

	//button onclick event
	//quit the game
	public void Exit ()
	{
		Application.Quit ();
	}
}
