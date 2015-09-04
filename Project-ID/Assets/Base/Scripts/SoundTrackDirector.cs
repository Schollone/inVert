using UnityEngine;
using System.Collections;

public class SoundTrackDirector : MonoBehaviour {

	public static int globalEnemiesTotal;
	public static int globalEnemiesNear;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//GatesMode ();
	}

	/*void GatesMode () {
		if (globalEnemiesNear >= 1) {
			SoundTrackMixer.cutOff = true;
		} else {
			SoundTrackMixer.cutOff = false;
		}
		if (globalEnemiesTotal >= 40) {
			SoundTrackMixer.heavy = true;
		} else {
			SoundTrackMixer.heavy = false;
		}
	}*/
}
