using UnityEngine;
using System.Collections;

public class MoveToPlayer : MonoBehaviour
{

	private Transform _target;
	private Rigidbody _rigid;
	public float speed = 100;
	
	void Start ()
	{
		_target = GameObject.FindGameObjectWithTag ("Player").transform;
		_rigid = GetComponent<Rigidbody> ();
	}

	private void FixedUpdate ()
	{
		_rigid.AddForce ((_target.position - transform.position).normalized * speed * Time.fixedDeltaTime);
	}
}
