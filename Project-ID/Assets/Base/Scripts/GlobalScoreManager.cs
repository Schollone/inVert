using UnityEngine;
using System.Collections;

public class GlobalScoreManager : MonoBehaviour {

	static public float globalScore = 0f;
	static public float globalMultiplier = 1f;
	static public float globalTime = 0f;
	static public float stopTime = 0f;

	// Use this for initialization

	void Start () {
		globalScore = 0f;
		globalMultiplier = 1f;
		globalTime = 0f;
		stopTime = 0f;
	}

	static public void AddScore (float score) {
		globalScore += score*globalMultiplier;
	}

	static public void AddMultiplier (float multiplier) {
		globalMultiplier += multiplier;
	}

	static public void SetStartTime() {
		globalTime = Time.time;
	}

	static public void SetStopTime() {
		stopTime = Time.time;
	}

	static public float GetTime() {
		float result = stopTime - globalTime;
		return result;
	}

	static public void Reset() {
		globalScore = 0f;
		globalMultiplier = 1f;
		globalTime = 0f;
		stopTime = 0f;
	}
}
