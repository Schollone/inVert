using UnityEngine;
using System.Collections;

public class ProjectileLocomotion : MonoBehaviour {

	public float speed = 5.0f;
	public float accuracy = 0.1f;

	private Vector3 _playerPosition;
	private Rigidbody _rigid;

	private void Awake() {
		_rigid =  GetComponent<Rigidbody>();
	}

	private void Update(){
		_playerPosition = PlayerController.PLAYER_POSITION;
		transform.forward = Vector3.Lerp(transform.forward,(_playerPosition-transform.position).normalized,accuracy);
	}

	private void FixedUpdate() {
		_rigid.AddForce(transform.forward*speed*Time.fixedDeltaTime);
	}





}
