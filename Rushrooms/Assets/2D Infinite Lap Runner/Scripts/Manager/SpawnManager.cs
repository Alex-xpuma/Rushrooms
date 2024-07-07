using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
struct Chance
{
	public Spawner spawner;
	public float probability; //the chance of spawning a certain object with a platform
}

//this script decide what other stuff will be spawned with a platform
public class SpawnManager : MonoBehaviour {

	[SerializeField] private float spawnNothingProbability; //the chance of not spawning anything with a platform
	[SerializeField] private Chance[] chances;

	private List<Spawner> spawnerList = new List<Spawner> (); //a list of Spawner scripts

	private List<float> cd = new List<float> (); //the cumulative distribution

	void Start ()
	{
		StructProcessor ();
	}
	
	void SpawnOtherStuff (Transform platform) //everytime a new platform is spawned, we will call this method there.
	{
		int index = BinarySearch.RandomNumberGenerator (cd); //draw a random number to decide what will be spawned this round

		if (spawnerList[index] != null) //check if there is a spawner script attached. As we don't need cunduct any script to spawn nothing, if it's null, we just do nothing. If it's not null, we execute the corresponding script.
			spawnerList[index].SendMessage ("SpawnObj", platform, SendMessageOptions.DontRequireReceiver); //we call the SpawnObj method
	}

	void StructProcessor () //convert the custom struct to a list and construct the the cumulative distribution
	{	
		float sum = 0;
		
		cd.Add (sum);

		sum = spawnNothingProbability;

		spawnerList.Add (null); //we don't need script to spawn nothing, so will add null
		cd.Add (sum);
		
		foreach (Chance chance in chances) //in this for loop, we add scripts of spawning differect objects into the spawnerList list
		{
			spawnerList.Add (chance.spawner);

			sum += chance.probability;

			cd.Add (sum);
		}

		if (sum == 0f)
		{
			cd[1] = 1f; //spawn nothing with platform
		}
		if (sum != 1f)
			cd = cd.ConvertAll (x => x /sum); //normalize cd, if it's not already normalized
	}
}
