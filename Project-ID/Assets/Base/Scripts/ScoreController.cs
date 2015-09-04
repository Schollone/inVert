using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

	private TextMesh score;
	private TextMesh multiplicator;

	// Use this for initialization
	void Start () {
		score = transform.GetChild (0).GetComponent<TextMesh> ();
		multiplicator = transform.GetChild (1).GetComponent<TextMesh> ();
		score.text = GlobalScoreManager.globalScore.ToString () + " pt.";
		multiplicator.text = "x" + GlobalScoreManager.globalMultiplier.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		score.text = GlobalScoreManager.globalScore.ToString () + " pt.";
		multiplicator.text = "x" + GlobalScoreManager.globalMultiplier.ToString ();
	}
}
