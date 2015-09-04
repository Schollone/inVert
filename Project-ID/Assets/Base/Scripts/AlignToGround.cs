using UnityEngine;
using System.Collections;

public class AlignToGround : MonoBehaviour {

	private Transform _massCenter;

	private void Awake ()
	{
		_massCenter = GameObject.FindGameObjectWithTag ("Sphere").transform;
		Align ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Align () {
		Vector3 direction = (transform.position - _massCenter.transform.position).normalized;
		Quaternion toRotate = Quaternion.FromToRotation (transform.up, -direction);
		transform.rotation = toRotate * transform.rotation;
	}
}
