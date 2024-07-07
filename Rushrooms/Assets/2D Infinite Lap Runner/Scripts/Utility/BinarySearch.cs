using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BinarySearch : MonoBehaviour {

	//Generate a random number by using the given cumulative distribution 
	public static int RandomNumberGenerator (List<float> cd)
	{
		float rnd;
		
		do
			rnd = Random.value;
		while (rnd == 0); //draw a random number between 0 (exclusive) and 1 (inclusive)
		
		//The remaing codes conduct a binary search to determine which interval does the random number lie in.
		//The returned value is actually the interval number minus 1, as list starts with subscript 0.
		int min = 0;
		int max = cd.Count - 1;
		int mid = 0;
		
		while (min <= max)
		{
			mid = (min + max) / 2;
			
			if (rnd == cd[mid])
				return mid - 1;
			else if (rnd < cd[mid])
			{
				max = mid - 1;
				
				mid--;
			}
			else
				min = mid + 1;
		}
		
		return mid;
	}
}
