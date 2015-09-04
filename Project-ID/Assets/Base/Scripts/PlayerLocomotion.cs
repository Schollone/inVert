using UnityEngine;
using System.Collections;

public class PlayerLocomotion : MonoBehaviour
{
	[SerializeField]
	private float
		_speed = 1000.0f;
	[SerializeField]
	private float
		_rotationSpeed = 3.0f;
	[SerializeField]
	private int
		_rotationalExponent = 2;
	[SerializeField]
	private float
		_jumpForce = 100f;
	[SerializeField]
	private float
		_initialJumpHeight = 20f;
	[SerializeField]
	private float
		driftThreshold;
	[SerializeField]
	private float
		driftForce;
	[SerializeField]
	private float
		forwardCap = 0.9f;
	[SerializeField]
	private float
		backwardCap = -0.9f;
	[SerializeField]
	private int 
		driftExponent = 4;
	private Rigidbody _rigid;
	private float _initialRotationSpeed;
	
	private void Awake ()
	{

		_rigid = GetComponent<Rigidbody> ();
		_initialRotationSpeed = _rotationSpeed;
	}

	public void Movement (float forwardInput, float sidewardInput)
	{
		if (forwardInput > forwardCap) {
			forwardInput = forwardCap;
		}

		if (forwardInput < backwardCap) {
			forwardInput = backwardCap;
		}

		//Original Movement
		_rigid.AddForce (transform.forward * forwardInput * _speed * Time.fixedDeltaTime, ForceMode.Force);

		//Exponentiel Rotation
		transform.RotateAround (transform.up, -((Mathf.Sign (sidewardInput) - 
			(Mathf.Sign (sidewardInput) * Mathf.Pow ((sidewardInput * _rotationSpeed) + Mathf.Sign (sidewardInput), _rotationalExponent))) 
			* Time.fixedDeltaTime));

		if (Mathf.Abs (sidewardInput) > driftThreshold) {
			_rigid.AddForce (transform.right * -Mathf.Sign (sidewardInput) * (Mathf.Abs (sidewardInput) - driftThreshold) * driftForce, ForceMode.Force);
		}
	}

	public void Jump ()
	{
		_rotationSpeed = (_rotationSpeed / 2);
		Vector3 force = transform.up * _jumpForce * (_rigid.velocity.magnitude + _initialJumpHeight) * Time.deltaTime;
		_rigid.AddForce (force, ForceMode.VelocityChange);
	}

	public void ResetRotationSpeed ()
	{
		_rotationSpeed = _initialRotationSpeed;
	}
}

	

