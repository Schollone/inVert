using UnityEngine;
using System.Collections;

public class GateBehaviour : MonoBehaviour
{
	public float destructionRadius = 200;
	public GameObject collectableToSpawn;
	public GameObject effectToSpawn;
	public GameObject enemyDeathEffect;
	public GameObject showRadius;
	
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			Instantiate (showRadius, transform.position, transform.rotation * Quaternion.Euler(-90,0,0));
			DestroyEnemies (transform.position, destructionRadius);
			Instantiate (effectToSpawn, transform.position, transform.rotation);
			GameObject.Destroy(transform.parent.gameObject);
		}
	}
	
	void DestroyEnemies (Vector3 center, float radius)
	{
		Collider[] hitColliders = Physics.OverlapSphere (center, radius);

		for (int i = 0; i<hitColliders.Length; i++) {
			if (hitColliders [i].gameObject.tag == "Enemy") {
			//	Instantiate (collectableToSpawn, (hitColliders[i].gameObject.transform.position), (hitColliders[i].gameObject.transform.rotation));
				Instantiate (enemyDeathEffect, (hitColliders[i].gameObject.transform.position), (hitColliders[i].gameObject.transform.rotation));
				DestroyObject (hitColliders [i].gameObject);
			}
		}
	}
}
