using UnityEngine;
using System.Collections;

[System.Serializable]
public class MinMaxValues
{
	public float min;
	public float max;
}

public class AimAtPlayer : MonoBehaviour
{
	private Transform _target;
	private bool _aimInFront = false;
	public float aimInFrontPropability = 0.1f;
	private float _aimInFrontStrength = 8.0f;
	public MinMaxValues aimInFrontValues = new MinMaxValues ();
	public float aimingSpeed = 0.1f;

	public bool getAimInFront ()
	{
		return _aimInFront;
	}

	void Start ()
	{
		_target = GameObject.FindGameObjectWithTag ("Player").transform;

		if ((Random.Range (1, 100)) <= aimInFrontPropability * 100) {
			_aimInFront = true;
			_aimInFrontStrength = Random.Range (aimInFrontValues.min, aimInFrontValues.max);
			Debug.Log (_aimInFrontStrength);
		}
	}

	void Update ()
	{
		Vector3 relativePos = _target.position - transform.position;

		if (_aimInFront == false) {
			transform.forward = Vector3.Lerp (transform.forward, relativePos.normalized, aimingSpeed);
		} else {
			transform.forward = Vector3.Lerp (transform.forward, (relativePos + _target.forward * _aimInFrontStrength).normalized, aimingSpeed);
		}
	}
}
