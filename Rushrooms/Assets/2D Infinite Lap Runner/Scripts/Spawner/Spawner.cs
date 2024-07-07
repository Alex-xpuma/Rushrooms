using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct Obj //as list or dictionary won't appear in the inspector, we create a custom struct to accomplish this
{
	public GameObject objPrefab;
	public int amount; //The initial amount of otherPrefab will be instantiated in the pool.
	public float probability;
}

[RequireComponent (typeof (Pool))] //make sure the object pooling script is attached
public class Spawner : MonoBehaviour {

	[SerializeField] private Obj[] objs;
	[SerializeField] protected float h; //the vertical displacement of the spawn position related to the position of platform

	private List<KeyValuePair<GameObject, int>> objList; //a list convert from the Obj struct array
	protected List<float> cd; //the cumulative distribution of platforms. It's used to decide which platform to spawn.
	protected Pool pool;

	protected virtual void Start ()
	{
		pool = GetComponent<Pool> ();

		StructProcessor ();

		pool.CreatePool (objList, transform);
	}

	//get an object from the pool and put at a certain position with a certain rotation
	void SpawnObj (Transform platform)
	{
		GameObject obj = pool.getPooledObject (BinarySearch.RandomNumberGenerator (cd));

		obj.transform.position = platform.TransformPoint (new Vector3 (0 , h, 0));

		obj.transform.rotation = platform.rotation;

		obj.SetActive (true);
	}

	/* How we pick up objects based on give probabilities:
	 * Let's say we have 3 different platforms with probability 1, 1, 2 respectively.
	 * In other words, platform1 and platform2 both have 25% chance to appear and 50% changce for platform3.
	 * In order to randomly pick up platforms based on the given probabilites,
	 * we need to create a cumulative distribution, i.e. (0 , 1, 2, 4), or (0, 0.25, 0.5, 1) after normalization (this is the one we will use).
	 * Everytime we need to spawn a platform, we generate a random number between 0(exclusive) and 1(inclusive). If it's not greater than 0.25 we spawn platform1, spawn platform2 if it's between 0.25(exclusive) and 0.5(inclusive), etc.
	 */
			
	/* How we calculate normalized cumulative distribution
	 * We create a new variable 'sum', and assign 0 as the first value of the cumulative distribution list.
	 * In each iteration of a for loop, we increase 'sum' by the probability of a specific object, and add the value to the cumulative distribution list.
	 * After we loop through all objects, we normalize the the cumulative distribution list simply by dividing each elements by the sum of all probabilities.
	 * 
	 * Let's still use the previous example, 3 platforms with probability 1, 1, 2.
	 * First, we create 'sum', and cd becomes (0).
	 * Then begin iterations.
	 * Itertaion 1: sum = 1, cd = (0, 1)
	 * Iteration 2: sum = 2, cd = (0, 1, 2)
	 * Iteration 3: sum = 4, cd = (0, 1, 2, 4)
	 * Finally, divide cd by sum, cd = (0, 0.25, 0.5, 1)
	 */

	void StructProcessor () 
	{	
		//make List instances
		objList = new List<KeyValuePair<GameObject, int>> (); 
		cd = new List<float> ();

		float sum = 0;

		cd.Add (sum);

		foreach (Obj obj in objs)
		{
			//Except calculating cumulative distribution, we create a list of platforms for object pooling process at the same time.
			//The list contains the platforms needed to pool, as well as the corrosponding amount.
			
			objList.Add (new KeyValuePair<GameObject, int> (obj.objPrefab, (obj.amount > 0) ? obj.amount : 1));
			
			sum += obj.probability;
			
			cd.Add (sum);
		}

		//if (sum == 0f)

		if (sum != 1f)
			cd = cd.ConvertAll (x => x /sum); //normalize cd, if it's not already normalized
	}


}
