using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {
	public AudioSource gotHit;
	GameObject SoundtrackMixer;


	void Awake () {
		SoundtrackMixer = GameObject.FindGameObjectWithTag ("SoundtrackMixer");
	}

	void Update ()
	{
		//DebugDisplayScores ();
	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Enemy") {
			if (GameManager.Instance.PlayerIsAlive) {
				Debug.Log("I died");
				GameManager.Instance.PlayerIsAlive = false;
				GlobalScoreManager.SetStopTime();
				gotHit.Play ();
				SoundtrackMixer.GetComponent<SoundTrackMixer>().PlayOutro();
				GameManager.Instance.SetGameState(GameState.HighscoreInput);
			}
			GameManager.Instance.PlayerIsAlive = false;

		}
	}

	/*void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Enemy") {
			if (_isAlive) {
				OnGameOver ();
			}
			_isAlive = false;
		}
	}*/
	void DebugDisplayScores ()
	{
		Debug.Log ("Score: " + GlobalScoreManager.globalScore);
		Debug.Log ("Multiplier: " + GlobalScoreManager.globalMultiplier);
	}

}
