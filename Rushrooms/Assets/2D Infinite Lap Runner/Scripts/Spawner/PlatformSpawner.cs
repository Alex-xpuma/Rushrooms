using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct Platform //as list or dictionary won't appear in the inspector, we create a custom struct to accomplish this
{
	public GameObject platformPrefab;
	public int amount; //The amount of platformPrefab will be instantiated in the pool
	public float probability; //the probability of the platform to appear in the scene
	public bool canSpawnAtStart; //if this gameobject can used at the start prepare stage
}

[RequireComponent (typeof (Pool))] //make sure the object pooling script is attached
public class PlatformSpawner : MonoBehaviour {

	[SerializeField] private Platform[] platforms; //a set of randomly spawned objects
	[SerializeField] private float platformLength; //the length of platforms
	[SerializeField] private int amountAtStart; //the number of platforms need to spawn at start. This value also is used to calculate checkPointDistance which decides how far the spawn point is. If you find objects are spawned on screen, please increase this value.

	private float groundRadius; //the radius of ground. We will get the value from GameManager script, so no need to set the value here.
	private Vector2 playerGroundPosition;
	private Quaternion playerGroundRotation;
	private Vector2 lastSpawnPoint; //the position where the latest platform spawned
	private Vector2 checkPoint; //We calculate a checkPoint position. If the distance between checkPoint and lastSpawnPoint is greater than platformLength, we spawn a new platform at checkPoint.
	private float checkPointDistance; //the distance between playerGroundPosition and checkPoint. This value is used to calculate checkPoint.
	private float sqrPlatformLength; //squared platformLength. We will compare this value with another squared value, since extracting a square has a better performance than extracting a root.
	private List<KeyValuePair<GameObject, int>> platformList; //a list convert from the platform struct array
	private List<float> cd; //the cumulative distribution of platforms. It's used to decide which platform to spawn.
	private List<float> cdAtStart; //the cumulative distribution of platforms. It's used to decide which platform to spawn at the start prepare stage.
	private Pool pool;
	private GameManager gameManager;
	private SpawnManager spawnManager;
	
	void Start ()
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();

		spawnManager = GameObject.Find ("GameManager").GetComponent<SpawnManager> ();

		pool = GetComponent<Pool> ();
		
		groundRadius = gameManager.groundRadius; //get the groundRadius from GameManager
		
		playerGroundPosition = gameManager.playerGroundPosition; //get the playerGroundPosition from GameManager
		
		playerGroundRotation = gameManager.playerGroundRotation; //get the playerGroundRotation from GameManager
		
		StructProcessor ();
		
		pool.CreatePool (platformList, transform);
		
		lastSpawnPoint = SpawnAtStart (amountAtStart);
		
		CalculateCheckPointDistance ();
		
		sqrPlatformLength = Mathf.Pow (platformLength - 0.2f, 2f);
	}
	
	void Update ()
	{
		playerGroundPosition = gameManager.playerGroundPosition;
		
		Quaternion rotation;
		
		checkPoint = Trigonometricf.GetPosition (groundRadius, groundRadius, checkPointDistance, playerGroundPosition, out rotation); 
		
		float sqrDistance = (checkPoint - lastSpawnPoint).sqrMagnitude; //the squared distance between checkPoing and lastSpawnPoint
		
		if (sqrDistance > sqrPlatformLength) //spawn a platform, if there is enough room to spawn one
		{
			GameObject obj = SpawnPlatform (cd, checkPoint, rotation);

			spawnManager.SendMessage ("SpawnOtherStuff", obj.transform, SendMessageOptions.DontRequireReceiver);
			
			lastSpawnPoint = checkPoint; //update lastSpawnPoint
		}
	}

	//spawn the platforms for the start stage. In other words, platforms created before the game starts.
	Vector2 SpawnAtStart (int amount)
	{
		SpawnPlatform (cdAtStart, playerGroundPosition, playerGroundRotation);
		
		Quaternion rotation;
		
		Vector2 lastSpawnPoint = playerGroundPosition;
		
		for (int i = 0; i < amount - 1; i++)
		{
			lastSpawnPoint = Trigonometricf.GetPosition (groundRadius, groundRadius, platformLength, lastSpawnPoint, out rotation);
			
			SpawnPlatform (cdAtStart, lastSpawnPoint, rotation);
		}

		return lastSpawnPoint;
	}

	//pick a platform based on cumulative distribution 'cd', and put it at given position with given rotation
	GameObject SpawnPlatform (List<float> cd, Vector2 position, Quaternion rotation)
	{
		GameObject obj = pool.getPooledObject (BinarySearch.RandomNumberGenerator (cd));
		
		obj.transform.position = position;
		
		obj.transform.rotation = rotation;
		
		obj.SetActive (true);

		return obj;
	}

	//calculate checkPointDistance. The value varies according plarformLength, groundRadius and amountAtStart.
	//For the mathematics behind, please check: coming soon
	void CalculateCheckPointDistance ()
	{
		float alpha = Mathf.Acos (1f - 0.5f * Mathf.Pow (platformLength / groundRadius, 2f));
		
		checkPointDistance = Mathf.Sqrt (2f - 2f * Mathf.Cos(amountAtStart * alpha)) * groundRadius;
	}

	//Calculate cumulative distributions and create a list for object pooling from the custom platform struct array
	void StructProcessor () 
	{	
		//make List instances
		platformList = new List<KeyValuePair<GameObject, int>> (); 
		cd = new List<float> ();
		cdAtStart = new List<float> ();

		float sum1 = 0;
		float sum2 = 0;
		
		cd.Add (sum1);
		cdAtStart.Add (sum2);
		
		foreach (Platform platform in platforms)
		{
			//Except calculating cumulative distribution, we create a list of platforms for object pooling process at the same time.
			//The list contains the platforms needed to pool, as well as the corrosponding amount.

			platformList.Add (new KeyValuePair<GameObject, int> (platform.platformPrefab, (platform.amount > 0) ? platform.amount : 1));
			
			sum1 += platform.probability;
			
			cd.Add (sum1);

			//The cumulative distribution for the start stage only contains these probabilites of platforms which can be spawned at start stage.
			//Therefore, if canSpawnAtStart is true, we add the probability as usual.
			//Otherwise, we treate the probability as 0. In other words, increasing sum by 0.
			sum2 = platform.canSpawnAtStart ? sum2 + platform.probability : sum2;
			
			cdAtStart.Add (sum2);
		}

		//if (sum1 == 0f)

		if (sum1 != 1f)
			cd = cd.ConvertAll (x => x /sum1); //normalize cd, if it's not already normalized

		//if sum2 is 0, which means no platform is allowed to spawn at start stage. We use cd to replace cdAtStart to make use we can spawn platforms.
		//You will never ever want this to happen, since you may not wish to spawn certain platforms at start stage.
		//For example, you definitely don't want to spawn a hole platform at the game beginning point.
		//As your character will fall into the hole and the game will over, if that happens.
		if (sum2 == 0f)
			cdAtStart = cd; 
		else if (sum2 != 1f)
			cdAtStart = cdAtStart.ConvertAll(x => x / sum2);
	}


}

