using UnityEngine;
using System.Collections;

/* Please ref to the GetPosition image to understand what this script does.
 * Basically, we have a triangle with lengths a, b and c. We also kown 2 vertics (0, 0) and (p.x, p.y).
 * This script helps us to calculate the other vertex and its rotation correspons to the position y axis.
 */
public class Trigonometricf : MonoBehaviour {

	public static Vector2 GetPosition (float a, float b, float c, Vector2 p, out Quaternion rotation)
	{
		float alpha = Mathf.Acos ((Mathf.Pow (a, 2f) + Mathf.Pow (b, 2f) - Mathf.Pow (c, 2f)) / (2 * a * b));

		float beta = Mathf.Atan2 (p.y, p.x);

		float angle = beta - alpha;

		rotation = Quaternion.Euler (new Vector3 (0, 0, angle * Mathf.Rad2Deg + 270));

		return new Vector2 (Mathf.Cos (angle) * b, Mathf.Sin (angle) * b);
	}

	//calculate the rotation of a given vector2
	public static Quaternion GetRotation (Vector2 position)
	{
		float z = Mathf.Atan2 (position.y, position.x) * Mathf.Rad2Deg - 90f;
		
		return Quaternion.Euler (new Vector3 (0, 0, z));
	}



}
