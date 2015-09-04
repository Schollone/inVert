using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	//TranslateMovement
/*	public float speed = 10.0f;
	public float rotationSpeed = 10.0f;*/

	//FakePhysics
/*	private float speed = 0;
	public float speedLimit = 100.0f;
	public float accleration = 2.0f;
	public float rotationSpeed = 0;
*/

	//RigidbodyForce & Move
	/*public float accleration = 10.0f;
	public float rotationSpeed = 10.0f;
	private Rigidbody rigid;
*/

	//CharacterController
/*	public float speed = 10.0f;
	public float rotationSpeed = 10.0f;
	private CharacterController controller;*/

	private void Awake ()
	{
		//rigid = GetComponent<Rigidbody> ();
		//controller = GetComponent<CharacterController> ();
	}

	private void Update ()
	{
		//	CharacterMovement ();
	}

	private void FixedUpdate ()
	{
/*		RigidbodyForce ();
		RigidbodyMove ();*/
	}

/*	private void TranslateMovement ()
	{
		transform.Rotate (transform.up * Input.GetAxis ("Horizontal") * rotationSpeed * Time.deltaTime);
		transform.Translate (transform.forward * Input.GetAxis ("Vertical") * speed * Time.deltaTime, Space.World);
	}*/

/*	private void FakePhysics ()
	{
		transform.Rotate (transform.up * Input.GetAxis ("Horizontal") * rotationSpeed * Time.deltaTime);

		speed += Input.GetAxis ("Vertical") * accleration * Time.deltaTime;  
		if (speed > speedLimit)
			speed = speedLimit;
		
		transform.Translate (transform.forward * speed, Space.World);
	}*/

/*	private void RigidbodyForce ()
	{
		rigid.AddForce (transform.forward * Input.GetAxis ("Vertical") * accleration * Time.fixedDeltaTime, ForceMode.Acceleration);
		//rigid.AddForce (transform.forward * Input.GetAxis ("Vertical") * accleration * Time.fixedDeltaTime, ForceMode.Force);
		//transform.Rotate (transform.up * Input.GetAxis ("Horizontal") * rotationSpeed * Time.fixedDeltaTime);
		rigid.AddTorque (transform.up * Input.GetAxis ("Horizontal") * rotationSpeed * Time.fixedDeltaTime);
	}*/

/*	private void RigidbodyMove ()
	{
		rigid.MovePosition (transform.position + transform.forward * Input.GetAxis ("Vertical") * accleration * Time.fixedDeltaTime);
		rigid.MoveRotation (rigid.rotation * Quaternion.Euler (transform.up * Input.GetAxis ("Horizontal") * rotationSpeed * Time.fixedDeltaTime));
	}*/

/*	private void CharacterMovement ()
	{
		controller.Move (transform.forward * Input.GetAxis ("Vertical") * speed * Time.fixedDeltaTime);
		transform.Rotate (transform.up * Input.GetAxis ("Horizontal") * rotationSpeed * Time.deltaTime);
	}*/
}
