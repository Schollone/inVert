using UnityEngine;
using System.Collections;

public class ActivateSimpleMoveToPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider collider) {
		if (collider.gameObject.tag == "Collectable") {
			collider.gameObject.GetComponent<SimpleMoveToPlayer> ().isMoving = true;
		}
	}
}
