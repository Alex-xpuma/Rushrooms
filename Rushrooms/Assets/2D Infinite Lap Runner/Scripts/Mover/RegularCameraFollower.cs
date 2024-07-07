using UnityEngine;
using System.Collections;

//script for regular scence, replacing CameraFollower
public class RegularCameraFollower : MonoBehaviour {

	public Transform player;
	public float distanceX;
	public float distanceY;
	
	private Transform thisTransform;
	private float z;

	void Start ()
	{
		thisTransform = transform;
		
		z = thisTransform.position.z;
	}
	
	void Update ()
	{
		thisTransform.position = new Vector3 (player.position.x + distanceX, player.position.y + distanceY, z);
	}
}
