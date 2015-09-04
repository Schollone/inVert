using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using WiimoteLib;

public class ChangeOnReady : MonoBehaviour
{
	public Sprite newImage;
	private bool _ready = false;

	private void Update ()
	{
		if (WiimoteReader.BalanceBoardIsReady == true && _ready == false) {
			GetComponent<Image> ().overrideSprite = newImage;
			_ready = true;
		}

		if (_ready == true && WiimoteReader.GetBalanceBoard ().GetAxis (WiimoteReader.Board.Axis.Vertical) > 0.35f) {
			LoadLevel.Load ("GatesGroundScene");
		}
	}
}
