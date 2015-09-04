using UnityEngine;
using System.Collections;

public class SpawnPrefab : MonoBehaviour {

	public GameObject objectToSpawn;

	GameObject _sphere;
	public bool onSurfaceOnly;
	public float radiusCheck;
	public float notificationTime;
	private Transform _massCenter;
	float lifeTime = 0;
	bool movedToAnotherLocation = false;
	float testTime;
	int tries = 0;
	private GameObject _enemie;
	private GameObject _collectables;
	private GameObject _gates;

	void Awake () {
		_massCenter = GameObject.FindGameObjectWithTag ("Sphere").transform;
		_sphere = GameObject.FindGameObjectWithTag ("Sphere");
		SpaceCheck (transform.position, radiusCheck);
		Align ();

		_enemie = GameObject.Find ("Enemies");
		if (_enemie == null) {
			_enemie = new GameObject("Enemies");
		}
		_collectables = GameObject.Find ("Collectables");
		if (_collectables == null) {
			_collectables = new GameObject("Collectables");
		}
		_gates = GameObject.Find ("Gates");
		if (_gates == null) {
			_gates = new GameObject("Gates");
		}
	}

	void Update () {
		CheckLifeTime ();
	}

	void SpawnObject () {
		GameObject spawnObject = (GameObject) GameObject.Instantiate (objectToSpawn, this.transform.position, this.transform.rotation);
		if (spawnObject.CompareTag("Enemy")) {
			spawnObject.transform.SetParent(_enemie.transform);
		} else if (spawnObject.CompareTag("Collectable")) {
			spawnObject.transform.SetParent(_collectables.transform);
		} else if (spawnObject.CompareTag("Gate")) {
			spawnObject.transform.SetParent(_gates.transform);
		}
	}


	void SpaceCheck (Vector3 center, float radius) {
		Collider[] hitColliders = Physics.OverlapSphere (center, radius);
		for (int i = 0; i<hitColliders.Length; i++) {
			if (hitColliders [i].gameObject.tag != "Sphere" && hitColliders [i].gameObject.tag != "Player") {
				MoveToAnotherLocation ();
				tries += 1;
				if (tries > 2) {
					DestroySelf ();
				}
			} else {
				Align ();
			}
		}
	}


	void MoveToAnotherLocation () {
		lifeTime = 0;
		if (onSurfaceOnly) {
			Vector3 randomPosition = _sphere.transform.position + Random.onUnitSphere * _sphere.transform.localScale.x * 0.0109f;
			transform.position = randomPosition;
			SpaceCheck (transform.position, radiusCheck);
		} else {
			Vector3 randomPosition = _sphere.transform.position + Random.insideUnitSphere * _sphere.transform.localScale.x * 0.01f;
			transform.position = randomPosition;
			SpaceCheck (transform.position, radiusCheck);
		}
		Align ();
	}


	void CheckLifeTime () {
		lifeTime += Time.deltaTime;
		if (lifeTime > notificationTime) {
			//SpaceCheck (transform.position, radiusCheck);
			//if (movedToAnotherLocation) {
			//	movedToAnotherLocation = false;
			//} else {
				DestroySelf ();
			//}
		}
	}


	void DestroySelf ()
	{
		if (tries < 3) {
			SpawnObject ();
		}
		DestroyObject (this.gameObject);
	}


	void Align () {
		Vector3 direction = (transform.position - _massCenter.transform.position).normalized;
		Quaternion toRotate = Quaternion.FromToRotation (transform.up, -direction);
		transform.rotation = toRotate * transform.rotation;
	}

}
