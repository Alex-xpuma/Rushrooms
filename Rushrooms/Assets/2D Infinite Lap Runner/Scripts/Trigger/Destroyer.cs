using UnityEngine;
using System.Collections;

//this script disactive all dynamic objects collide with the destroyer collider
public class Destroyer : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Destroyable")
		{
			other.gameObject.SetActive(false);
		}
	}
}
