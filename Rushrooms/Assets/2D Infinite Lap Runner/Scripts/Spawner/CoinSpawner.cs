using UnityEngine;
using System.Collections;

public class CoinSpawner : Spawner {

	[SerializeField] private float gap; //the gap between each coin in one coin group
	[SerializeField] private float maxLength; //the maximum length of a coin group
	[SerializeField] private float a; //amplitude of the cosine curve
	
	private float w; //adjust the period of the cosine curve
	private int maxAmount; //the maximum number of coins in one coin group

	protected override void Start ()
	{
		w = Mathf.PI / maxLength;

		maxAmount = Mathf.CeilToInt (maxLength / gap);

		base.Start ();
	}
	
	void SpawnObj (Transform platform)
	{
		int n = Random.Range (1, maxAmount); //the number of coins to spawn

		float localX = -(n - 1) * gap / 2f; //the local(related to the platform) x position of the first coin

		int index = BinarySearch.RandomNumberGenerator (cd); //random number, decide which kind of coin to spwan if there are more than 1 kind of coins

		float rndA = Random.Range (1, a); //random amplitude

		float rndH = Random.Range (1, h); //random heightf

		//a loop to spawn each coin in the coin group
		for (int i = 0; i < n; i++)
		{
			GameObject obj = pool.getPooledObject (index);

			Vector3 localPosition = new Vector3 (localX, rndA * Mathf.Cos(w * localX) + rndH, 0); //the position is calculated based on a cosine function

			obj.transform.position = platform.TransformPoint (localPosition); //convert the local position to world position
			
			obj.transform.rotation = platform.rotation;
			
			obj.SetActive (true);

			localX += gap; //update the local x for next coin
		}
	}
}
