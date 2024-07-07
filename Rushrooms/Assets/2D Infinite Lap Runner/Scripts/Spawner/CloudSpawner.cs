using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Pool))]
public class CloudSpawner : MonoBehaviour {

	[SerializeField] private GameObject[] clouds;
	[SerializeField] private float minY; //minimum y axis value of the spawning point (local position of background)
	[SerializeField] private float maxY; //maximum y asix value (local position of background)
	[SerializeField] private float x; //x axis value of the spawning point (local position of background)
	[SerializeField] private float interval; //seconds needed to wait before spawning a new cloud
	[SerializeField] private float minSpeed; //minimum cloud speed
	[SerializeField] private float maxSpeed; //maximum cloud speed

	private Pool pool;

	void Start ()
	{
		pool = GetComponent<Pool> ();

		List<KeyValuePair<GameObject, int>> cloudList = new List<KeyValuePair<GameObject, int>> ();

		foreach (GameObject cloud in clouds) //create a list which is needed by the object pooling script
			cloudList.Add(new KeyValuePair<GameObject, int> (cloud, 2));

		pool.CreatePool (cloudList, transform);

		StartCoroutine (SpawnCloud ());
	}
	
	IEnumerator SpawnCloud ()
	{
		while (true)
		{
			Vector2 position = new Vector2 (x, Random.Range (minY, maxY)); //random spawn position

			int index = Random.Range (0, clouds.Length); //random number, decide which cloud to spawn

			GameObject cloud = pool.getPooledObject (index); //get the cloud from the pool

			cloud.transform.localPosition = position; //change the positon, since the 'position' is a local position related to the background, we need to change the local position of the cloud. (Because clouds are child objects of background)

			cloud.GetComponent<CloudMover> ().speed = Random.Range (minSpeed, maxSpeed); //pick a random speed for the cloud

			cloud.SetActive (true);

			yield return new WaitForSeconds (interval); //wait several seconds before spawning a new cloud
		}
	}
}
