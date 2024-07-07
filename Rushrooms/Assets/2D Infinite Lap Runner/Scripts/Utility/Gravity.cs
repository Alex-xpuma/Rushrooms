using UnityEngine;
using System.Collections;

//this script simulated a planetary gravity
public class Gravity : MonoBehaviour {

	[SerializeField] private float g = -10f; //gravitational acceleration
	[SerializeField] private GameObject player;

	private Vector2 direction;

	void Update ()
	{
		if (player.GetComponent<Rigidbody2D>() != null) //this script only affects object with a rigidbody2D component
		{
			direction = player.transform.position;

			player.transform.rotation = Trigonometricf.GetRotation (direction); //change the rotation, make the object look like standing on the planet
		}
	}

	void FixedUpdate ()
	{
		if (player.GetComponent<Rigidbody2D>() != null)
		{
			Vector2 force = direction.normalized * g * player.GetComponent<Rigidbody2D>().mass; //calcuate the force. Newton's law: force = mass * acceleration
			
			player.GetComponent<Rigidbody2D>().AddForce (force); //apply the force
		}
	}

	//The following code is a more generic simulation of gravity, but due to some Unity bug, this will cause OnTriggerEnter2D doesn't get called constantly,
	//i.e. triggers on pickups won't get called constantly. We will use the above workaround in the meantime.
	/*void OnTriggerStay2D (Collider2D other)
	{
		if (other.rigidbody2D != null && other.tag != "NoGravity")
		{
			Vector2 direction = other.transform.position;

			Vector2 force = direction.normalized * g * other.rigidbody2D.mass;

			other.rigidbody2D.AddForce (force);

			other.transform.rotation = Trigonometricf.GetRotation (direction);
		}
	}*/
}
