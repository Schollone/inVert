using UnityEngine;
using System.Collections;

public class HighscoreInputController : MonoBehaviour {

	private TextMesh _name;

	// Use this for initialization
	void Start () {
		_name = GameObject.Find("UIName").GetComponent<TextMesh>();
		_name.text = "_ _ _";
	}
	
	// Update is called once per frame
	void Update () {
		DisplayNameInput();
	}

	private void DisplayNameInput() {
		char[] name = HighscoreManager.Instance.getPlayerName().ToCharArray();
		string s = "";
		foreach(char c in name) {
			s += c.ToString() + " ";
		}
		_name.text = s;
	}
}
