using UnityEngine;
using System.Collections;

public class CollectableBehaviour : MonoBehaviour
{

	private void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			//Punkte
			//Healing
			GameObject.DestroyObject (this.gameObject);
			Debug.Log ("Collected");
		}
	}
	private void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Enemy") {
			//Punkte
			Debug.Log ("BÄHM!");
			GameObject.DestroyObject (other.gameObject);
			GameObject.DestroyObject (this.gameObject);
		}
	}
}
