using UnityEngine;
using System.Collections;

public class AddPointsOnDestroy : MonoBehaviour {

	public int points = 100;
	public bool addMultipliactorOnDestroy = false;
	public float multiplier = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy () {
		if (GameManager.Instance.PlayerIsAlive) {
			Debug.Log("Add Score");
			GlobalScoreManager.AddScore(points);
			if (addMultipliactorOnDestroy) {
				GlobalScoreManager.AddMultiplier(multiplier);
			}  
		}                  
	}
}
