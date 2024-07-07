using UnityEngine;
using System.Collections;

//script for regular scence, replacing BackgroundFollower
public class RegularBackgroundFollower : MonoBehaviour {

	public Transform player;
	
	private Transform thisTransform;
	private float y, z;
	
	void Start ()
	{
		thisTransform = transform;

		y = thisTransform.position.y;

		z = thisTransform.position.z;
	}
	
	void Update ()
	{
		thisTransform.position = new Vector3 (player.position.x, y, z); //set the new position of background
	}
}
