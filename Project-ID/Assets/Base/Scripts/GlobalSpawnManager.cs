using UnityEngine;
using System.Collections;

public class GlobalSpawnManager : MonoBehaviour {


	public int startOnBeat = 8;
	public int endOnBeat = 12;
	GameObject _sphere;
	public GameObject spawnNotifier;
	public GameObject spawningPrefab;
	public int startRate = 8;
	public int endRate = 1;
	public int changeRateAfterSpawn = 1;
	public int startAmount = 1;
	public int raiseAmountBy = 1;
	public int endAmount = 4;
	public int changeAmountAfterSpawn = 1;
	public bool onSurfaceOnly = true;
	public float radiusCheck = 6;
	public float notificationTime = 3.5294375f;
	float beatLength = 3.5294375f;
	bool beatpoint1 = false;
	bool beatpoint2 = false;
	bool beatpoint4 = false;
	bool beatpoint8 = false;
	bool beatpointIsSet1 = false;
	bool beatpointIsSet2 = false;
	bool beatpointIsSet4 = false;
	bool beatpointIsSet8 = false;
	Vector3 spherePosition;
	float sphereRadius;
	int spawnCounterA = 0;
	int spawnCounterB = 0;

	float soundTrackTime = 0;

	private GameObject _notifier;

	// Use this for initialization
	void Awake () {
		_sphere = GameObject.FindGameObjectWithTag ("Sphere");
		spherePosition = _sphere.transform.position;
		sphereRadius = _sphere.transform.localScale.x;

		_notifier = GameObject.Find ("Notifier");
		if (_notifier == null) {
			_notifier = new GameObject("Notifier");
		}
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		soundTrackTime += Time.deltaTime;
		if (soundTrackTime / beatLength >= startOnBeat && soundTrackTime / beatLength <= endOnBeat) {
			IncreaseRate ();
			SetBeatPoint ();
			SpawnByBeat ();
		}
	}

	void IncreaseRate () {
		if (spawnCounterA >= changeRateAfterSpawn) {
			spawnCounterA = 0;
			if (startRate > endRate) {
				if (startRate == 8) {
					startRate = 4;
					return;
				}
				if (startRate == 4) {
					startRate = 2;
					return;
				}
			}
				if (startRate == 2) {
					startRate = 1;
				} else if (startRate < endRate) {
					if (startRate == 1) {
						startRate = 2;
						return;
					}
					if (startRate == 2) {
						startRate = 4;
						return;
					}
					if (startRate == 4) {
						startRate = 8;
				} 
			}
		}
	}

	void IncreaseAmount () {
		if (spawnCounterB >= changeAmountAfterSpawn) {
			spawnCounterB = 0;
			if (startAmount < endAmount) {
				startAmount += raiseAmountBy;
			} else if (startAmount > endAmount) {
				startAmount -= raiseAmountBy;
			}
		}
	}

	void SpawnByBeat () {
		if (startRate == 1 && beatpoint1) {
			Spawn ();
			beatpoint1 = false;
		}
		if (startRate == 2 && beatpoint2) {
			Spawn ();
			beatpoint2 = false;
		}
		if (startRate == 4 && beatpoint4) {
			Spawn ();
			beatpoint4 = false;
		}
		if (startRate == 8 && beatpoint8) {
			Spawn ();
			beatpoint8 = false;
		}
	}

	void Spawn () {
		if (onSurfaceOnly) {
			for(int i = 0; i < startAmount; i++)
			{
				SpawnGameObjectOnSurface ();
			}
		} else {
			for(int i = 0; i < startAmount; i++)
			{	
				SpawnGameObjectInSpace ();
			}
		}
	}

	void SpawnGameObjectInSpace () {
		Vector3 randomPosition = spherePosition + Random.insideUnitSphere * sphereRadius * 0.01f;
		Quaternion randomRotation = Random.rotation;
		GameObject notifier = (GameObject) Object.Instantiate (spawnNotifier, randomPosition, randomRotation);
		notifier.transform.SetParent(_notifier.transform);
		notifier.GetComponent<SpawnPrefab> ().objectToSpawn = spawningPrefab;
		notifier.GetComponent<SpawnPrefab> ().onSurfaceOnly = false;
		notifier.GetComponent<SpawnPrefab> ().radiusCheck = radiusCheck;
		notifier.GetComponent<SpawnPrefab> ().notificationTime = notificationTime;
	}

	void SpawnGameObjectOnSurface () {
		Vector3 randomPosition = spherePosition + Random.onUnitSphere * sphereRadius * 0.0109f;
		Quaternion randomRotation = Random.rotation;
		GameObject notifier = (GameObject) Object.Instantiate (spawnNotifier, randomPosition, randomRotation);
		notifier.transform.SetParent(_notifier.transform);
		notifier.GetComponent<SpawnPrefab> ().objectToSpawn = spawningPrefab;
		notifier.GetComponent<SpawnPrefab> ().onSurfaceOnly = true;
		notifier.GetComponent<SpawnPrefab> ().radiusCheck = radiusCheck;
		notifier.GetComponent<SpawnPrefab> ().notificationTime = notificationTime;
	}

	void SetBeatPoint () {
		if (soundTrackTime % (beatLength) < 0.1) {
			if (!beatpointIsSet1) {
				beatpoint1 = true;
				beatpointIsSet1 = true;
			}
			if (!beatpointIsSet2) {
				beatpoint2 = true;
				beatpointIsSet2 = true;
			}
			if (!beatpointIsSet4) {
				beatpoint4 = true;
				beatpointIsSet4 = true;
			}
			if (!beatpointIsSet8) {
				beatpoint8 = true;
				beatpointIsSet8 = true;
			}
		} else {
			beatpoint1 = false;
			beatpoint2 = false;
			beatpoint4 = false;
			beatpoint8 = false;
			if ((soundTrackTime % (beatLength)) > (beatLength)-0.1) {
				beatpointIsSet1 = false;
			}
			if ((soundTrackTime % (beatLength * 2)) > (beatLength*2)-0.1) {
				beatpointIsSet2 = false;
			}
			if ((soundTrackTime % (beatLength * 4)) > (beatLength*4)-0.1) {
				beatpointIsSet4 = false;
			}
			if ((soundTrackTime % (beatLength * 8)) > (beatLength*8)-0.1) {
				beatpointIsSet8 = false;
			}
		}
		
	}
}
