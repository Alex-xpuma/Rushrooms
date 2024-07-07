using UnityEngine;
using System.Collections;

//script for regular scence, replacing CloudMover
public class RegularCloudMover : CloudMover {

	protected override void Update ()
	{
		transform.Translate (-Vector3.right * Time.deltaTime * speed);
		
		if (thisTransform.localPosition.x <= -6f) //when the cloud leaves the screen (-6 happens to be the value in my scene), we disactive it.
			gameObject.SetActive (false);
	}
}
