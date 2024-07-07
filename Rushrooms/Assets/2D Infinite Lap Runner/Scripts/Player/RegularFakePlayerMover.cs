using UnityEngine;
using System.Collections;

//script for regular scence, replacing FakePlayerMover
public class RegularFakePlayerMover : FakePlayerMover {

	void OnEnable ()
	{
		transform.position = new Vector3 (transform.position.x, 2f, 0f);
	}

	protected override void Update ()
	{
		thisTransform.Translate (Vector3.right * speed * Time.deltaTime);
	}
}
