using UnityEngine;
using System.Collections;

public class HideWarning : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("Show", 3f);

	}

	private void Show() {
		this.transform.GetChild(0).gameObject.SetActive(true);
		Invoke("Hide", 6f);
	}

	private void Hide() {
		this.transform.GetChild(0).gameObject.SetActive(false);
	}
}
