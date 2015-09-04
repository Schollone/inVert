using UnityEngine;
using System.Collections;

public class SimpleMoveToPlayer : MonoBehaviour {

	public float speed = 600;
	public bool isMoving = false;
	GameObject _target;
	public float multiplier = 0.5f;

	public AudioSource positiveFeedback;
	public AudioClip[] feedbackSounds;
	public bool playing = false;

	void Awake () {
		_target = GameObject.FindGameObjectWithTag ("Player");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving) {
			Moving ();
		}
	
	}

	void Moving () {
		Vector3 direction = (_target.transform.position - transform.position).normalized;
		transform.position += direction * Time.deltaTime * speed;
		if ((transform.position - _target.transform.position).magnitude < 4) {
			GlobalScoreManager.AddMultiplier(multiplier);
			positiveFeedback.clip = feedbackSounds[Random.Range(0, feedbackSounds.Length)];
			if (playing == false) {
				playing = true;
				positiveFeedback.Play ();
			}
			if (positiveFeedback.isPlaying == false) {
				DestroyObject(this.gameObject);
			}
		}
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.tag == "Collector") {
			isMoving = true;
		}
	}
}
