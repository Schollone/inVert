using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighscoreController : MonoBehaviour {

	[SerializeField] private GameObject _uiEntryPrefab;

	private GameObject _table;

	private HighscoreManager _highscoreManager;

	// Use this for initialization
	void Start () {
		_table = GameObject.Find("UITable");
		_highscoreManager = HighscoreManager.Instance;
		_table.transform.Rotate (-25f, 0f, 0f);
		Debug.Log("HighscoreController");
	}
	
	// Update is called once per frame
	void Update () {
		if (_highscoreManager.updatedHighscore) {
			_highscoreManager.updatedHighscore = false;
			Debug.LogWarning("update Highscore");
			DisplayHighscoreList();
		}
	}

	void DisplayHighscoreList () {
		Player[] players = HighscoreManager.Instance.getPlayers ().ToArray ();
		Debug.Log("Length: " + players.Length);
		for (int i = 0; i < players.Length; i++) {
			Player p = players [i];
			GameObject uiEntry = GameObject.Instantiate (_uiEntryPrefab, _table.transform.position, _table.transform.rotation) as GameObject;
			uiEntry.transform.SetParent (_table.transform);
			uiEntry.transform.Translate (0f, (float)-1.39 * i, 0f);
			TextMesh name = uiEntry.transform.GetChild (0).GetComponent<TextMesh> ();
			TextMesh score = uiEntry.transform.GetChild (1).GetComponent<TextMesh> ();
			TextMesh time = uiEntry.transform.GetChild (2).GetComponent<TextMesh> ();
			name.text = p.name;
			score.text = p.score.ToString ();
			string someString = string.Format("{0:0}:{1:00}", Mathf.Floor(p.time/60), p.time % 60);
			time.text =  someString;
		}

	}
}
