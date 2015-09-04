using UnityEngine;
using System.Collections;

public class ToggleHunting : MonoBehaviour
{

	public static bool huntig = false;
	//bool audioClipSwapped = false;
	//public AudioClip normalClip;
	//public AudioClip alertClip;
	//public AudioSource enemyNoise;


	private void Awake ()
	{
		huntig = false;
	}

	private void Update ()
	{
		if (GetComponent<OrientateTowardsPlayer> ().isActiveAndEnabled != huntig) {
			GetComponent<OrientateTowardsPlayer> ().enabled = huntig;
			if (huntig == true) {
				GetComponent<GroundLocomotion> ().ChangeSpeed (true);
				/*if (!audioClipSwapped) {
					if (enemyNoise.clip == normalClip) {
						enemyNoise.clip = alertClip;
						enemyNoise.Play ();
						audioClipSwapped = true;
					}
				}*/
			} else {
				GetComponent<GroundLocomotion> ().ChangeSpeed (false);
				/*if (audioClipSwapped) {
					if (enemyNoise.clip == 	alertClip) {
						enemyNoise.clip = normalClip;
						enemyNoise.Play ();
						audioClipSwapped = true;
					}
				}*/
			}
		}
	}


}
