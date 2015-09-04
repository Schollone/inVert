using UnityEngine;
using System.Collections;

public class SetBackSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy () {
		SoundTrackMixer.noEnemyDestroyed = 0;
	}
}
