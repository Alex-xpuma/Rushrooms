using UnityEngine;
using System.Collections;

//this script let the background follow the player
public class BackgroundFollower : MonoBehaviour {

	[SerializeField] private Transform player;

	private GameManager gameManager;
	private Transform thisTransform;
	private float z;
	private Vector2 playerGroundPosition;

	void Start ()
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> (); //find the GameManager script from an object called GameManager

		thisTransform = transform;

		z = thisTransform.position.z; //as the z axis is never going to change, we store it in a variable instead of using transform.position.z everytime
	}

	void Update ()
	{
		playerGroundPosition = gameManager.playerGroundPosition; //get the value from GameManager script

		thisTransform.position = new Vector3 (playerGroundPosition.x, playerGroundPosition.y, z); //set the new position of background

		thisTransform.rotation = player.rotation; //set the new rotation of background
	}
}
