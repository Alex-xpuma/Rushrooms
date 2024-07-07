using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//script for regular scence, replacing PlatformSpawner
public class RegularPlatformSpawner : MonoBehaviour {

	public Platform[] platforms;
	public float platformLength;
	public int amountAtStart;
	public Transform player;

	private Vector2 playerGroundPosition;
	private Vector2 lastSpawnPoint;
	private Vector2 checkPoint;
	private Vector2 checkPointDistance;
	private float sqrPlatformLength;
	private List<KeyValuePair<GameObject, int>> platformList;
	private List<float> cd;
	private List<float> cdAtStart;
	private Pool pool;
	private SpawnManager spawnManager;

	void Start ()
	{
		spawnManager = GameObject.Find ("GameManager").GetComponent<SpawnManager> ();
		
		pool = GetComponent<Pool> ();
		
		playerGroundPosition = new Vector2 (player.position.x, 0f);

		StructProcessor ();
		
		pool.CreatePool (platformList, transform);
		
		lastSpawnPoint = SpawnAtStart (amountAtStart);
		
		CalculateCheckPointDistance ();
		
		sqrPlatformLength = Mathf.Pow (platformLength - 0.2f, 2f);
	}
	
	void Update ()
	{
		playerGroundPosition = new Vector2 (player.position.x, 0f);

		checkPoint = playerGroundPosition + checkPointDistance;
		
		float sqrDistance = (checkPoint - lastSpawnPoint).sqrMagnitude;
		
		if (sqrDistance > sqrPlatformLength)
		{
			GameObject obj = SpawnPlatform (cd, checkPoint);
			
			spawnManager.SendMessage ("SpawnOtherStuff", obj.transform, SendMessageOptions.DontRequireReceiver);
			
			lastSpawnPoint = checkPoint;
		}
	}
	
	Vector2 SpawnAtStart (int amount)
	{
		SpawnPlatform (cdAtStart, playerGroundPosition);

		Vector2 lastSpawnPoint = playerGroundPosition;
		
		for (int i = 0; i < amount - 1; i++)
		{
			lastSpawnPoint.x += platformLength;
			
			SpawnPlatform (cdAtStart, lastSpawnPoint);
		}
		
		return lastSpawnPoint;
	}
	
	GameObject SpawnPlatform (List<float> cd, Vector2 position)
	{
		GameObject obj = pool.getPooledObject (BinarySearch.RandomNumberGenerator (cd));
		
		obj.transform.position = position;

		obj.SetActive (true);
		
		return obj;
	}
	
	void CalculateCheckPointDistance ()
	{
		checkPointDistance = new Vector2 (amountAtStart * platformLength, 0f);
	}
	
	void StructProcessor () 
	{	
		platformList = new List<KeyValuePair<GameObject, int>> (); 
		cd = new List<float> ();
		cdAtStart = new List<float> ();
		
		float sum1 = 0;
		float sum2 = 0;
		
		cd.Add (sum1);
		cdAtStart.Add (sum2);
		
		foreach (Platform platform in platforms)
		{
			platformList.Add (new KeyValuePair<GameObject, int> (platform.platformPrefab, (platform.amount > 0) ? platform.amount : 1));
			
			sum1 += platform.probability;
			
			cd.Add (sum1);
			
			sum2 = platform.canSpawnAtStart ? sum2 + platform.probability : sum2;
			
			cdAtStart.Add (sum2);
		}

		if (sum1 != 1f)
			cd = cd.ConvertAll (x => x /sum1);
		
		if (sum2 == 0f)
			cdAtStart = cd; 
		else if (sum2 != 1f)
			cdAtStart = cdAtStart.ConvertAll(x => x / sum2);
	}

}
