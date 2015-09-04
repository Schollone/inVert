using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour
{
	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player") {
			DestroyObject (other.gameObject);		//call fancy Destruction stuff 
			DestroyObject (this.gameObject);
		}
		if (other.gameObject.tag == "Sphere")
			DestroyObject (this.gameObject);
	}
}
