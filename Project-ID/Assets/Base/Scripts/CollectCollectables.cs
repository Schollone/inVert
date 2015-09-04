using UnityEngine;
using System.Collections;

public class CollectCollectables : MonoBehaviour
{
	public float multiplier = 0.5f;


	void Update () {
		DebugDisplayScores ();
	}

	void DebugDisplayScores () {
		Debug.Log ("Score: " + GlobalScoreManager.globalScore);
		Debug.Log ("Multiplier: " + GlobalScoreManager.globalMultiplier);
	}

	private void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Collectable") {
			DestroyObject (other.gameObject);	//maybe call fancy Explosion funktion with Destruction in it?
			GlobalScoreManager.AddMultiplier(multiplier);
		}
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.tag == "Collectable") {
			collider.gameObject.GetComponent<SimpleMoveToPlayer>().isMoving = true;
		}
	}
}
