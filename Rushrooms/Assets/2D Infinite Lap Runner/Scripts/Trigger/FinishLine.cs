using UnityEngine;
using System.Collections;

//script for a trigger at the finish line
public class FinishLine : MonoBehaviour {

	[SerializeField] private ScoreManager scoreManager;

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Player") //when player finishes a lap, call the IncreaseLapCountByOne method to adjust multipiler and UI text
		{
			scoreManager.SendMessage ("IncreaseLapCountByOne", SendMessageOptions.DontRequireReceiver);
		}
	}
}
