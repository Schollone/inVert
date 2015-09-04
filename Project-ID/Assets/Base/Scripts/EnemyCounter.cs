using UnityEngine;
using System.Collections;

public class EnemyCounter : MonoBehaviour {

	bool isNear = false;

	void Awake () {
		SoundTrackDirector.globalEnemiesTotal += 1;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy () {
		if (isNear) {
			SoundTrackDirector.globalEnemiesNear -= 1;
		}
		SoundTrackDirector.globalEnemiesTotal -= 1;
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.tag == "Collector") {
			SoundTrackDirector.globalEnemiesNear += 1;
			isNear = true;
		}
	}

	void OnTriggerExit (Collider collider) {
		if (collider.gameObject.tag == "Collector") {
			SoundTrackDirector.globalEnemiesNear -= 1;
			isNear = false;
		}
	}
}
