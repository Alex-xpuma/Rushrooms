using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* object pooling script
 * We make a list of objects which will be used in the game.
 * Instantiate these objects by the given amount and disactive them
 * 
 * When we need to use certain objects, we simplely active these pooled objects instead of Instantiating on the fly.
 * This allow us to speed up the program, as constantly instantiating and destroying objects are performance expensive.
 */ 
public class Pool : MonoBehaviour
{
	private List<List<GameObject>> pooledObjects = new List<List<GameObject>> ();

	public void CreatePool (List<KeyValuePair<GameObject, int>> list, Transform parent)
	{
		foreach (KeyValuePair <GameObject, int> entry in list)
		{
			List<GameObject> subPool = new List<GameObject> ();
			
			for (int i = 0; i < entry.Value; i ++)
			{
				GameObject obj = (GameObject)Instantiate (entry.Key);
				
				obj.SetActive (false);
				
				obj.transform.parent = parent;
				
				subPool.Add (obj);
			}
			
			pooledObjects.Add (subPool);
		}
	}

	public GameObject getPooledObject (int i, bool canGrow = true)
	{
		for (int j = 0; j < pooledObjects[i].Count; j++)
		{
			if (!pooledObjects[i][j].activeInHierarchy)
				return pooledObjects[i][j];
		}

		if (canGrow)
		{
			GameObject obj = (GameObject)Instantiate (pooledObjects[i][0]);

			obj.transform.parent = pooledObjects[i][0].transform.parent;

			pooledObjects[i].Add (obj);

			return obj;
		}

		return null;
	}
}
