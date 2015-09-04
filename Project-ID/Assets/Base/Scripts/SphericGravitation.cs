using UnityEngine;
using System.Collections;

public class SphericGravitation : MonoBehaviour
{
	private Transform _massCenter;
	private Rigidbody _rigid;
	private bool _grounded = false;

	public float gravitation = 9.81f;

	private void Awake ()
	{
		_massCenter = GameObject.FindGameObjectWithTag ("Sphere").transform;
		_rigid = GetComponent<Rigidbody> ();
	}

	private void FixedUpdate ()
	{
		if (transform != null && _massCenter.transform != null) {
			Vector3 direction = (transform.position - _massCenter.transform.position).normalized;
			
			if (_grounded == false)
				_rigid.AddForce (direction * gravitation * Time.fixedDeltaTime);
			
			Quaternion toRotate = Quaternion.FromToRotation (transform.up, -direction);
			transform.rotation = toRotate * transform.rotation;
		}
	}

	private void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Sphere") {
			_grounded = true;
		}
	}

	private void OnCollisionExit (Collision other)
	{
		if (other.gameObject.tag == "Sphere") {
			_grounded = false;
		}
	}
}