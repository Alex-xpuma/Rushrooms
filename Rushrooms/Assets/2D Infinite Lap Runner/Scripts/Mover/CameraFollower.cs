using UnityEngine;
using System.Collections;

//this script let the camera follow the player
public class CameraFollower : MonoBehaviour {

	[SerializeField] private Transform player;
	[SerializeField] private float distanceX; //adjust the horizontal position of the camera
	[SerializeField] private float distanceY; //adjust the vertical position of the camera
	
	private Transform thisTransform;
	private float z;
	Vector2 target;
	
	void Start ()
	{
		thisTransform = transform;
		
		z = thisTransform.position.z; //store the z axis value as it won't change
	}
	
	void Update ()
	{
		float playerRadius = player.position.magnitude; //varialbe needed by Trigonometricf.GetPosition method
		
		float cameraRadius = playerRadius + distanceY; //varialbe needed by Trigonometricf.GetPosition method
		
		Quaternion rotation; //variable to store the out rotation
		
		target = Trigonometricf.GetPosition (playerRadius, cameraRadius, distanceX, player.position, out rotation); //calcuate the position where the camera should be and its rotation
		
		thisTransform.position = new Vector3 (target.x, target.y, z); //change the position
		
		thisTransform.rotation = rotation; //change the rotation
	}
}
