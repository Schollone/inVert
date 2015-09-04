using UnityEngine;
using System.Collections;

public class SpawnOverTime : MonoBehaviour
{

	[SerializeField] private float spawnPerSecond = 1.0f;
	[SerializeField] private GameObject prefabToSpawn;

	private GameObject _trailParticles;

	void Awake() {
		_trailParticles = GameObject.Find("TrailParticles");
		if (_trailParticles == null) {
			_trailParticles = new GameObject("TrailParticles");
		}
	}

	private void Start ()
	{
		InvokeRepeating ("Spawn", 1 / spawnPerSecond, 1 / spawnPerSecond);
	}

	private void Spawn ()
	{
		GameObject trailParticles = (GameObject) GameObject.Instantiate (prefabToSpawn, transform.position, transform.rotation);
		trailParticles.transform.SetParent(_trailParticles.transform);
	}
}
