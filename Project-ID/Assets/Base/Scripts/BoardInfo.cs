using UnityEngine;
using System.Collections;

public class BoardInfo : MonoBehaviour {

	private static BoardInfo _instance = null;

	private bool _readyToPlay = false;

	public static BoardInfo Instance {
		get {
			if(_instance == null) {
				_instance = GameObject.FindObjectOfType<BoardInfo>();
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}
	
	public void OnApplicationQuit() {
		BoardInfo._instance = null;
	}

	void Awake() {
		if(_instance == null) {
			_instance = this;
			DontDestroyOnLoad(this);
		} else {
			if(this != _instance) {
				Destroy(this.gameObject);
			}
		}
	}

	void Start () {
		Hide ();
	}

	void Update() {
		if (WiimoteReader.BalanceBoardIsReady && !_readyToPlay) {
			//Debug.LogWarning("BalanceBoardIsReady");
			Show ();
			if (PlayerIsOnBoard()) {
				//Debug.LogWarning("Steht auf Board");
				_readyToPlay = true;
				Hide();
			}
		}
	}

	private bool PlayerIsOnBoard() {
		bool result = (WiimoteReader.GetBalanceBoard() != null) &&
			(WiimoteReader.GetBalanceBoard ().GetAxisRaw (WiimoteReader.Board.AxisRaw.TopRight) > 30) && 
			(WiimoteReader.GetBalanceBoard ().GetAxisRaw (WiimoteReader.Board.AxisRaw.TopRight) > 30);
		return result;
	}
	
	private void Show() {
		this.transform.GetChild(0).gameObject.SetActive(true);
		//_readyToPlay = true;
		//Invoke("Hide", 4f);
	}
	
	private void Hide() {
		this.transform.GetChild(0).gameObject.SetActive(false);
	}
}
