using UnityEngine;
using System.Collections;

//this script will keep the fake player running by updating its position
public class FakePlayerMover : MonoBehaviour {

	public float speed;

	protected Transform thisTransform;

	void Start ()
	{
		thisTransform = transform;
	}

	protected virtual void Update ()
	{
		thisTransform.RotateAround (Vector3.zero, Vector3.forward, -speed * Time.deltaTime);
	}
}
