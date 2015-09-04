using UnityEngine;
using System.Collections;

public class CameraPointer : MonoBehaviour {

	public float waitTime = 2f;

	private RaycastHit hit;
	private GameObject _focusedLetter;
	private TextMesh _textMesh;

	private IEnumerator _coroutine;

	private GameManager GM;

	void Awake () {
		GM = GameManager.Instance;
	}

	private void ShowHighscoreInput () {
		Debug.Log ("Show Highscore Input");
		GM.SetGameState(GameState.HighscoreInput);
	}

	private void ShowHighscore () {
		Debug.Log ("Show Highscore");
		GM.SetGameState(GameState.Highscore);
	}

	private void SavePlayer () {
		ResetFocus ();
		ShowHighscore ();
	}

	private void ShowMenu() {
		GM.SetGameState(GameState.MainMenu);
	}
	
	private void StartGame() {
		GM.SetGameState(GameState.Game);
	}

	// Update is called once per frame
	void Update () {
		//Debug.Log (this.transform.forward);

		Ray ray = new Ray (this.transform.position, this.transform.forward);

		if (Physics.Raycast (ray, out hit)) {

			if (hit.collider != null) {
				GameObject button = hit.collider.gameObject;

				if (!button.Equals (_focusedLetter)) {
					//Debug.Log("Hitpoint changed");

					switch (button.name) {
					case "UIStart":
						SelectButton(button, StartGameCoroutine());
						break;
					case "UIHighscore":
						SelectButton(button, ShowHighscoreCoroutine());
						break;
					case "UISavePlayer":
						SelectButton(button, SavePlayerCoroutine());
						break;
					case "UIDeleteLetter":
						SelectButton(button, DeleteLetterCoroutine());
						break;
					case "UIAddLetter":
						SelectButton(button, AddLetterCoroutine());
						break;
					case "UIRestart":
						SelectButton(button, StartGameCoroutine());
						break;
					case "UICloseHighscore":
						SelectButton(button, ShowMenuCoroutine());
						break;
					default:
						//Debug.Log ("Hit something else");
						ResetFocus();
						break;
					}
				}
			}
		} else {
			Debug.Log ("Nothing hit !!!!!!!!!!!!!!!!");
			ResetFocus ();
		}
	}

	private void ResetFocus () {
		ResetCoroutine ();
		ResetTextColor ();
		_focusedLetter = null;
	}

	private void ResetCoroutine () {
		if (_coroutine != null) {
			StopCoroutine (_coroutine);
			_coroutine = null;
		}
	}

	private void ResetTextColor () {
		if (_focusedLetter != null) {
			_focusedLetter.GetComponent<TextMesh> ().color = Color.white;
		}
	}

	private void SelectButton(GameObject button, IEnumerator function) {
		ResetFocus();
		
		_textMesh = button.GetComponent<TextMesh> ();
		if (_textMesh != null) {
			_textMesh.color = Color.yellow;
			_focusedLetter = button;
			
			_coroutine = function;
			StartCoroutine (_coroutine);
		}
	}

	private bool IsAllowedToContinue() {
		if (_textMesh != null) {
			if (_focusedLetter != null && hit.collider != null && hit.collider.gameObject.Equals (_focusedLetter)) {
				_textMesh.color = Color.green;
				return true;
			}
		}
		return false;
	}

	private IEnumerator StartGameCoroutine () {
		yield return new WaitForSeconds (waitTime);
		
		if (IsAllowedToContinue()) {
			Invoke ("StartGame", 0.5f);
		}
	}

	private IEnumerator AddLetterCoroutine () {
		yield return new WaitForSeconds (waitTime);

		if (IsAllowedToContinue()) {
			HighscoreManager.Instance.AddLetter (_textMesh.text.ToCharArray () [0]);
			Invoke ("ResetFocus", 0.5f);
		}
	}

	private IEnumerator DeleteLetterCoroutine ()	{
		yield return new WaitForSeconds (waitTime);

		if (IsAllowedToContinue()) {
			HighscoreManager.Instance.DeleteLetter ();
			Invoke ("ResetFocus", 0.5f);			
		}
	}

	private IEnumerator SavePlayerCoroutine () {
		yield return new WaitForSeconds (waitTime);

		if (IsAllowedToContinue()) {
			string name = HighscoreManager.Instance.getPlayerName ();
			float score = GlobalScoreManager.globalScore;
			float time = GlobalScoreManager.GetTime();
			HighscoreManager.Instance.SavePlayer (name, score, time);
			Invoke ("SavePlayer", 0.5f);
		}
	}

	private IEnumerator ShowHighscoreCoroutine () {
		yield return new WaitForSeconds (waitTime);
		
		if (IsAllowedToContinue()) {
			Invoke ("ShowHighscore", 0.5f);
		}
	}

	private IEnumerator ShowMenuCoroutine () {
		yield return new WaitForSeconds (waitTime);

		if (IsAllowedToContinue()) {
			Invoke ("ShowMenu", 0.5f);
		}
	}
}