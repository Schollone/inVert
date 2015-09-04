using UnityEngine;
using System.Collections;

public class SpawnByDeath : MonoBehaviour
{
	public GameObject prefabToSpawn;

	private GameObject _particleExplosions;
	private bool _isQuitting = false;

	void Awake() {
		_particleExplosions = GameObject.Find ("ParticleExplosions");
		if (_particleExplosions == null) {
			_particleExplosions = new GameObject("ParticleExplosions");
		}
	}

	void OnApplicationQuit() {
		_isQuitting = true;
	}

	void OnDestroy ()
	{
		if (!_isQuitting) {
			Debug.Log(prefabToSpawn);
			GameObject particleExplosion = (GameObject) GameObject.Instantiate (prefabToSpawn, transform.position, transform.rotation);
			particleExplosion.transform.SetParent(_particleExplosions.transform);
		}
	}

}
