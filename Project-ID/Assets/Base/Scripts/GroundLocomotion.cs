using UnityEngine;
using System.Collections;

public class GroundLocomotion : MonoBehaviour
{

	private Rigidbody _rigid;
	
	private float _speed = 100.0f;

	public float defaultSpeed = 40000.0f;
	public float huntingSpeed = 75000.0f;

	public enum Modes
	{
		Straight,
		Hunt,
		Flee
	}
	;

	public Modes MovementMode;

	
	private void Awake ()
	{
		_rigid = GetComponent<Rigidbody> ();
	}

	private void Start() {
		_speed = defaultSpeed;
	}
	

	
	protected void FixedUpdate ()
	{
		switch (MovementMode) {
		case Modes.Hunt:
			transform.forward = (transform.forward + (PlayerController.PLAYER_POSITION - transform.position).normalized).normalized;
			break;
		case Modes.Flee:
			transform.forward = ((transform.forward + (transform.position - PlayerController.PLAYER_POSITION).normalized)).normalized;
			break;
		}

		_rigid.AddForce (transform.forward * _speed * Time.fixedDeltaTime);

	}

	public void ChangeSpeed(bool hunting) {
		if (hunting == true) {
			_speed = huntingSpeed;
		} else {
			_speed = defaultSpeed;
		}
	}
}
