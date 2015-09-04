using UnityEngine;
using System.Collections;

public class SoundTrackMixer : MonoBehaviour {

	
	public ProgressiveColorChange sphereScript;

	static public float noEnemyDestroyed = 0;
	bool wentHeavy = false;

	public AudioSource clock;
	public AudioSource humming;
	public AudioSource kick;
	public AudioSource hiHat;
	public AudioSource clap;
	public AudioSource effects;
	public AudioSource lead01Base;
	public AudioSource lead01;
	public AudioSource lead01Extra;
	public AudioSource lead02Base;
	public AudioSource lead02;
	public AudioSource lead02Rythm;
	public AudioSource lead02Extra;
	public AudioSource outro;


	public bool cutOff;
	public bool heavy;
	public float maxVolume = 1;

	float soundTrackTime = 0;
	public bool kickDA = false;
	public bool clapDA = false;
	public bool hiHatDA = false;
	public bool clockDA = false;
	public bool lead01DA = false;
	public bool lead01BaseDA = false;
	float beatLength = 3.52945f; //3.5294375f; 3.52945f; 3.5295f;
	public bool beatpoint1 = false;
	public bool beatpoint2 = false;
	public bool beatpoint4 = false;
	public bool beatpoint8 = false;
	public bool beatpointIsSet1 = false;
	public bool beatpointIsSet2 = false;
	public bool beatpointIsSet4 = false;
	public bool beatpointIsSet8 = false;

	bool toggleRed = false;
	int nextColor = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		noEnemyDestroyed += Time.deltaTime;

		if (noEnemyDestroyed >= 7.059 && soundTrackTime > 60) {
			cutOff = true;
		} else {
			cutOff = false;
		}



		if (soundTrackTime / beatLength > 24 && soundTrackTime / beatLength < 28) {
			GoIntoHeavyMode ();
		} else if (soundTrackTime / beatLength < 44) {
			GoIntoNormalMode ();
		} else if (soundTrackTime / beatLength < 48) {
			GoIntoHeavyMode ();
		} else if (soundTrackTime / beatLength < 54) {
			GoIntoNormalMode ();
		} else if (soundTrackTime / beatLength < 58) {
			GoIntoHeavyMode ();
		} else if (soundTrackTime / beatLength < 64) {
			GoIntoNormalMode ();
		} else if (soundTrackTime / beatLength < 68) {
			GoIntoHeavyMode ();
		} else if (soundTrackTime / beatLength < 74) {
			GoIntoNormalMode ();
		} else if (soundTrackTime / beatLength < 78) {
			GoIntoHeavyMode ();
		} else if (soundTrackTime / beatLength < 84) {
			GoIntoNormalMode ();
		} else if (soundTrackTime / beatLength < 88) {
			GoIntoHeavyMode ();
		} else {
			GoIntoNormalMode ();
		}


		SetBeatPoint ();
		soundTrackTime += Time.deltaTime; //3.75f

		if (soundTrackTime < 60) {
			Arrange ();
		}
		if (cutOff) {
			CutOffSound ();
		} else {
			if (soundTrackTime > 60) {
				DropInSound ();
			}
		}
		if (heavy) {
			HeavySound ();
		} else {
			NormalSound ();
		}
	}

	void Arrange () {
		if (soundTrackTime >= 7.059) {
			FadeIn (humming,0.1f);
		}
		if (soundTrackTime >= 14.114) {
			FadeIn (kick,1f);
		}
		if (soundTrackTime >= 28.235) {
			Activate (hiHat);
		}
		if (soundTrackTime >= 42.353) {
			Activate (clap);
		}
		if (soundTrackTime >= 56.231) { //56.466
			FadeIn (effects,0.1f);
			Activate (lead01Base);
			FadeIn (lead01,0.1f);
		}
	}

	void CutOffSound () {
			if (!kickDA) {
				Deactivate (kick);
			}
			if (beatpoint1) {
				if (!clapDA) {
					Deactivate (clap);
				}
				if (beatpoint1) {
					if (!hiHatDA) {
						Deactivate (hiHat);
					}
					if (beatpoint1) {
						if (!clockDA) {
							Deactivate (clock);
						}
						if (beatpoint1) {
							if (!lead01DA) {
								Deactivate (lead01);
							}
							if (beatpoint1) {
								if (!lead01BaseDA) {
								beatpoint1 = false;
								beatpoint2 = false;
								beatpoint4 = false;
								beatpoint8 = false;
								}
							if (beatpoint1) {
								DropInSound ();
							}
							lead01BaseDA = true;
						}
						lead01DA = true;
					}
					clockDA = true;
				}
				hiHatDA = true;
			}
			clapDA = true;
		}
		kickDA = true;
	}

	void DropInSound () {
		Activate (kick);
		Activate (clap);
		Activate (hiHat);
		Activate (clock);
		Activate (lead01);
		Activate (lead01Base);
		kickDA = false;
		clapDA = false;
		hiHatDA = false;
		clockDA = false;
		lead01DA = false;
		lead01BaseDA = false;
	}

	void HeavySound () {
		//if (beatpoint1) {
			Deactivate (hiHat);
			Deactivate (lead01Base);
			Deactivate (lead01);
			Activate (lead02Base);
			Activate (lead02);
			Activate (lead02Rythm);
			Activate (lead02Extra);
		//}
	}

	void NormalSound () {
		//if (beatpoint1) {
			Deactivate (lead02Base);
			Deactivate (lead02);
			Deactivate (lead02Rythm);
			Deactivate (lead02Extra);
		//}
	}

	void Activate (AudioSource cue) {
		cue.volume = maxVolume;
	}

	void Deactivate (AudioSource cue) {
		cue.volume = 0;
		beatpoint1 = false;
		beatpoint2 = false;
		beatpoint4 = false;
		beatpoint8 = false;
	}

	void FadeIn (AudioSource cue, float speed) {
		if (cue.volume <= maxVolume) {
			cue.volume = Mathf.Lerp (cue.volume, maxVolume, Time.deltaTime*speed);
		} else {
			Activate (cue);
		}
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
			if ((soundTrackTime % beatLength) > (beatLength)-0.1) {
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

	void GoIntoHeavyMode (){
		if (!wentHeavy) {
			ToggleHunting.huntig = true;
			heavy = true;
			wentHeavy = true;
			nextColor += 1;
			if (!toggleRed){
				sphereScript.SetRandomTargetColor(6);
				toggleRed = true;
			} else {
				sphereScript.SetRandomTargetColor(5);
				toggleRed = false;
			}

		}
	}

	void GoIntoNormalMode (){
		if (wentHeavy) {
			ToggleHunting.huntig = false;
			heavy = false;
			wentHeavy = false;
			sphereScript.SetRandomTargetColor(nextColor);
		}
	}
	public void PlayOutro () {
		outro.Play ();
		clock.Stop ();
		humming.Stop ();
		kick.Stop ();
		hiHat.Stop ();
		clap.Stop ();
		effects.Stop ();
		lead01Base.Stop ();
		lead01.Stop ();
		lead01Extra.Stop ();
		lead02Base.Stop ();
		lead02.Stop ();
		lead02Rythm.Stop ();
		lead02Extra.Stop ();
	}



}
