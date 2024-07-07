using UnityEngine;
using System.Collections;

//this script moves the clouds in the background
public class CloudMover : MonoBehaviour {

	[HideInInspector] public float speed; //how fast will the clound move
	protected Transform thisTransform;

	void Start ()
	{
		thisTransform = transform;
	}

	protected virtual void Update ()
	{
		thisTransform.RotateAround (Vector2.zero, Vector3.forward, speed * Time.deltaTime); //make the cloud rotate around the center (0, 0)

		thisTransform.localRotation = Quaternion.identity; //let the local lotation of clound keep being (0, 0, 0)

		if (thisTransform.localPosition.x <= -6f) //when the cloud leaves the screen (-6 happens to be the value in my scene), we disactive it.
			gameObject.SetActive (false);
	}
}
