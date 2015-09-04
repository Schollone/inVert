using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float lookSensitivity = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localRotation = Quaternion.Euler(Mathf.LerpAngle(transform.localRotation.eulerAngles.x,325 - 45 * Input.GetAxis("VerticalCam"),Time.deltaTime*lookSensitivity),Mathf.LerpAngle(transform.localRotation.eulerAngles.y,90 * Input.GetAxis("HorizontalCam"),Time.deltaTime*lookSensitivity), 0); 
	}
}