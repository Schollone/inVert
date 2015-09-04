using UnityEngine;
using System.Collections;
using WiimoteLib;

public class PlayerController : MonoBehaviour
{
	private PlayerLocomotion _locomotion;
	private PlayerAudio _audioScript;
	private bool _wiiReady = false;
	private float _sphereRadius;
	private float _playerHeight;

	public bool forceKeys = false;

	public static Vector3 PLAYER_POSITION;

	private void Awake ()
	{
		_locomotion = GetComponent<PlayerLocomotion> ();
		_audioScript = GetComponent<PlayerAudio> ();
	}

	private void Start ()
	{
		_sphereRadius = GameObject.Find ("Sphere").gameObject.transform.GetComponent<Collider> ().bounds.size.x;
		_playerHeight = transform.GetComponent<Collider> ().bounds.size.y;
	}

	private void FixedUpdate ()
	{
		float forwardInput = 0;
		float sidewardInput = 0;

		if (WiimoteReader.UseWiiBoard) {
			_wiiReady = !double.IsNaN (WiimoteReader.GetBalanceBoard ().GetAxis (WiimoteReader.Board.Axis.Top));
		}

		if (_wiiReady) {
			forwardInput = WiimoteReader.GetBalanceBoard ().GetAxis (WiimoteReader.Board.Axis.Vertical);
			sidewardInput = WiimoteReader.GetBalanceBoard ().GetAxis (WiimoteReader.Board.Axis.Horizontal);

		}

		if (forceKeys) {
			forwardInput = Input.GetAxis ("Vertical");
			sidewardInput = Input.GetAxis ("Horizontal");

		}

		_locomotion.Movement (forwardInput, sidewardInput);
		_audioScript.SetInput (forwardInput, sidewardInput);

		PLAYER_POSITION = transform.position;
	}

	/*
	 * Method checks distance between player and sphere origin (0,0,0) and 
	 * returns true if the distance is less than the distance to the sphere border.
	 */
	public bool IsGrounded ()
	{
		bool result = false;
		float distance = Vector3.Distance (transform.position, Vector3.zero);
		float groundDistance = (float)(((int)_sphereRadius >> 1) - _playerHeight);
		if (distance >= groundDistance) {
			result = true;
		}
		return result;
	}

}
