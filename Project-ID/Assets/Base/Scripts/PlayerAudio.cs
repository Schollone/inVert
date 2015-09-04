using UnityEngine;
using System.Collections;

public class PlayerAudio : MonoBehaviour {

	Rigidbody playersRigidbody;
	float currentVelocity;
	public AudioSource mid;
	public AudioSource crash;
	public float maxVelocity = 100f;
	public float floatingVolume = 0.5f;
	public float forwardInput; //brauch ich möglicherweise nicht
	public float sidewardInput; //brauch ich möglicherweise nicht

	public AudioClip[] crashes;

	// Use this for initialization
	void Start () {
		playersRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		GetCurrentVelocity ();
		SetVolume ();
		SetPitch ();
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag != "Sphere") {
			crash.volume = collision.relativeVelocity.magnitude/1000;
			crash.clip = crashes[Random.Range(0, crashes.Length)];
			crash.Play ();
		}
	}

	void GetCurrentVelocity () {
		currentVelocity = playersRigidbody.velocity.magnitude / maxVelocity;
	}

	void SetVolume () {
		mid.volume = currentVelocity*floatingVolume;
	}

	void SetPitch () {
		mid.pitch = currentVelocity;
	}

	public void SetInput(float forwardInput, float sidewardInput)
	{
		this.forwardInput = forwardInput;
		this.sidewardInput = sidewardInput;
	}

}
