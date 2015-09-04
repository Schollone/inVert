using UnityEngine;
using System.Collections;

public class OrientateTowardsPlayer : MonoBehaviour
{
	public float interpolationValue = 0.01f;
	private Transform _target;
	
	void Start ()
	{
		_target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update ()
	{
		Vector3 relativePos = _target.position - transform.position;
		transform.forward = Vector3.Lerp (transform.forward, relativePos, interpolationValue);
	}
}
