using UnityEngine;
using System.Collections;

public class ProgressiveColorChange : MonoBehaviour {

	public Material mainColor;
	public Material polesColor;
	public Material enemyColor;
	public Color[] alternateMainColor;
	public Color[] alternatePoleColors;
	public Color[] alternateEnemyColors;

	public Color currentColor;
	public Color targetColor;
	public Color currentPoleColor;
	public Color targetPoleColor;
	public Color currentEnemyColor;
	public Color targetEnemyColor;

	float changeTime = 0;
	public int nextColorScheme = 0;

	void Start () {
		currentColor = mainColor.color;
		currentPoleColor = polesColor.color;
		currentEnemyColor = enemyColor.color;
	}

	// Update is called once per frame
	void Update () {
		UpdateSphereColor ();

		if (Input.GetKeyDown (KeyCode.Space)) {
			nextColorScheme = nextColorScheme + 1;
			if (nextColorScheme > 6) {
				nextColorScheme = 0;
			}
			SetRandomTargetColor (nextColorScheme);
			changeTime = 0;
		}
	}

	public void SetRandomTargetColor (int colorScheme) {
		if (colorScheme > alternateMainColor.Length - 1) {
			colorScheme = 0;
		}
		targetColor = alternateMainColor [colorScheme];
		targetPoleColor = alternatePoleColors [colorScheme];
		targetEnemyColor = alternateEnemyColors [colorScheme];
		changeTime = 0;
	}

	void UpdateSphereColor () {
		if (changeTime < 4) {
			currentColor = Color.Lerp (currentColor, targetColor, Time.deltaTime);
			mainColor.color = currentColor;
			currentPoleColor = Color.Lerp (currentPoleColor, targetPoleColor, Time.deltaTime);
			polesColor.color = currentPoleColor;
			currentEnemyColor = Color.Lerp (currentEnemyColor, targetEnemyColor, Time.deltaTime);
			enemyColor.color = currentEnemyColor;
			changeTime = changeTime + Time.deltaTime;
		}
	}
}


