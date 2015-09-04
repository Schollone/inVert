using UnityEngine;
using System;
using System.Collections;
using WiimoteLib;

public class WiimoteBalanceBoard
{
	private float oaTopLeft, oaTopRight, oaBottomLeft, oaBottomRight = 0f;

	private float rwWeight, rwTopLeft, rwTopRight, rwBottomLeft, rwBottomRight;

	private bool _setCenterOffset = true;

	private float factor;

	private float _horizontalRestPercentage = 0f;
	private float _verticalRestPercentage = 0f;
	private float _verticalOffset = 0.2f;
	private float _outdatedWeight;
	private float _jumpOffset;

	public WiimoteBalanceBoard (float horizonatlRestPercentage, float verticalRestPercentage, float verticalOffset)
	{
		this.rwWeight = this.rwTopLeft = this.rwTopRight = this.rwBottomLeft = this.rwBottomRight = this._outdatedWeight = 0;
		factor = 1;
		_jumpOffset = 10f;
		this._horizontalRestPercentage = horizonatlRestPercentage;
		this._verticalRestPercentage = verticalRestPercentage;
		this._verticalOffset = verticalOffset;
	}

	public void SetData (WiimoteClassLib.BalanceBoardMessage b)
	{
		if (_setCenterOffset) {
			_setCenterOffset = setOffset ();
		}
		this.rwWeight = b.getWeight ();
		this.rwTopLeft = b.getWeightTopLeft () + oaTopLeft;
		this.rwTopRight = b.getWeightTopRight () + oaTopRight;
		this.rwBottomLeft = b.getWeightBottomLeft () + oaBottomLeft;
		this.rwBottomRight = b.getWeightBottomRight () + oaBottomRight;
	}

	private bool setOffset ()
	{
		float rwTopLeft = this.rwTopLeft;
		float rwTopRight = this.rwTopRight;
		float rwBottomLeft = this.rwBottomLeft;
		float rwBottomRight = this.rwBottomRight;

		var rwHighest = Math.Max (Math.Max (rwTopLeft, rwTopRight), Math.Max (rwBottomLeft, rwBottomRight));

		oaTopLeft = rwHighest - rwTopLeft;
		oaTopRight = rwHighest - rwTopRight;
		oaBottomLeft = rwHighest - rwBottomLeft;
		oaBottomRight = rwHighest - rwBottomRight;

		//Console.Write("OaTopLeft: " + oaTopLeft + " OaTopRight: " + oaTopRight + " OaBottomLeft: " + oaBottomLeft + " OaBottomRight: " + oaBottomRight);

		if (oaTopLeft == 0 && oaTopRight == 0 && oaBottomLeft == 0 && oaBottomRight == 0)
			return true;
		else
			return false;
	}

	public float GetAxisRaw (WiimoteReader.Board.AxisRaw input)
	{
		float value = 0;
		
		switch (input) {
		case WiimoteReader.Board.AxisRaw.TopRight:
			value = rwTopRight;
			break;
		case WiimoteReader.Board.AxisRaw.TopLeft:
			value = rwTopLeft;
			break;
		case WiimoteReader.Board.AxisRaw.BottomRight:
			value = rwBottomRight;
			break;
		case WiimoteReader.Board.AxisRaw.BottomLeft:
			value = rwBottomLeft;
			break;
		case WiimoteReader.Board.AxisRaw.Weight:
			value = rwWeight;
			break;
		}
		return value;
	}
	
	/// <summary>
	/// Returns the value of the virtual axis identified by DataReader.Axis.Value
	/// 
	/// The value will be in the range -1...1.
	/// </summary>
	public float GetAxis (WiimoteReader.Board.Axis input)
	{
		float total = rwTopRight + rwTopLeft + rwBottomRight + rwBottomLeft;
		float value = 0;
		
		switch (input) {
		case WiimoteReader.Board.Axis.Horizontal:
			float right = (rwTopRight + rwBottomRight) / total;
			float left = (rwTopLeft + rwBottomLeft) / total;
			float totalHorizontal = Mathf.Abs (right) + Mathf.Abs (left);

			float horizontalMovement = ((right - left) / totalHorizontal);
			value = horizontalMovement;
			if (horizontalMovement >= -_horizontalRestPercentage && horizontalMovement <= _horizontalRestPercentage) { // (von -0.2 bis 0.2 keine Bewegung)
				value = 0;
			}
			break;
		case WiimoteReader.Board.Axis.Vertical:
			float top = (rwTopRight + rwTopLeft) / total;
			float bottom = (rwBottomLeft + rwBottomRight) / total;
			float totalVertical = Mathf.Abs (top) + Mathf.Abs (bottom);
			
			float verticalMovement = ((top - bottom) / totalVertical);
			value = verticalMovement + _verticalOffset;
			if (verticalMovement >= -_verticalRestPercentage && verticalMovement <= _verticalRestPercentage) { // (von -0.2 bis 0.2 keine Bewegung)
				value = 0;
			}
			break;
		case WiimoteReader.Board.Axis.Top:
			float totalTop = rwTopRight + rwTopLeft;
			float topRight = rwTopRight / totalTop;
			float topLeft = rwTopLeft / totalTop;
			value = topRight - topLeft;
			break;
		case WiimoteReader.Board.Axis.Bottom:
			float totalBot = rwBottomRight + rwBottomLeft;
			float botRight = rwBottomRight / totalBot;
			float botLeft = rwBottomLeft / totalBot;
			value = botRight - botLeft;
			break;
		case WiimoteReader.Board.Axis.Right:
			float totalRight = rwTopRight + rwBottomRight;
			float botRight2 = rwBottomRight / totalRight;
			float topRight2 = rwTopRight / totalRight;
			value = topRight2 - botRight2;
			break;
		case WiimoteReader.Board.Axis.Left:
			float totalLeft = rwTopLeft + rwBottomLeft;
			float botLeft2 = rwBottomRight / totalLeft;
			float topLeft2 = rwTopRight / totalLeft;
			value = topLeft2 - botLeft2;
			break;
		}
		return value;
	}
	
	/// <summary>
	/// Returns true while the user holds down the button identified by DataReader.Button.Value.
	/// </summary>
	public bool GetButton (WiimoteReader.Board.Button input)
	{
		bool value = false;
		
		switch (input) {
		case WiimoteReader.Board.Button.Left:
			if (GetAxis (WiimoteReader.Board.Axis.Horizontal) < 0)
				value = true;
			break;
		case WiimoteReader.Board.Button.Right:
			if (GetAxis (WiimoteReader.Board.Axis.Horizontal) > 0)
				value = true;
			break;
		case WiimoteReader.Board.Button.TopLeft:
			if (GetAxis (WiimoteReader.Board.Axis.Vertical) > 0 && GetAxis (WiimoteReader.Board.Axis.Horizontal) < 0)
				value = true;
			break;
		case WiimoteReader.Board.Button.TopRight:
			if (GetAxis (WiimoteReader.Board.Axis.Vertical) > 0 && GetAxis (WiimoteReader.Board.Axis.Horizontal) > 0)
				value = true;
			break;
		case WiimoteReader.Board.Button.BottomLeft:
			if (GetAxis (WiimoteReader.Board.Axis.Vertical) < 0 && GetAxis (WiimoteReader.Board.Axis.Horizontal) < 0)
				value = true;
			break;
		case WiimoteReader.Board.Button.BottomRight:
			if (GetAxis (WiimoteReader.Board.Axis.Vertical) < 0 && GetAxis (WiimoteReader.Board.Axis.Horizontal) > 0)
				value = true;
			break;
		case WiimoteReader.Board.Button.Jump:
			//Debug.Log("TopLeft: " + rwTopLeft + "TopRight: " + rwTopRight + " BotLeft: " + rwBottomLeft + " BotRight: " + rwBottomRight);
			//Debug.Log(GetAxis(Axis.Vertical));
                /*if (GetAxis(WiimoteReader.Board.Axis.Vertical) > 0.7)
				    value = true;
			    break;*/
			float currentWeight = GetAxisRaw (WiimoteReader.Board.AxisRaw.TopLeft) + GetAxisRaw (WiimoteReader.Board.AxisRaw.TopRight);
			if (currentWeight >= 50f && currentWeight > (_outdatedWeight + _jumpOffset)) {
				value = true;
			}
			_outdatedWeight = currentWeight;
			break;
		}
		return value;
	}

	public void SetJumpOffset (float weightOffset)
	{
		this._jumpOffset = weightOffset;
	}

	public void SetCenterOffset ()
	{
		_setCenterOffset = true;
	}
	
	public bool AnyButton ()
	{
		return false;
	}

	public float GetAxisAdapted (WiimoteReader.Board.Axis input)
	{
		// den Faktor hier irgendwie anpassen, sodass die Beschleunigung schneller ist, wenn man schon länger läuft
		float value = GetAxis (input) * factor;
		
		return value;
	}
}
