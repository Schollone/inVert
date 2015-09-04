using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	private static EventManager _eventManager;

	public delegate void HuntingEvent ();
	public event HuntingEvent OnStartHunting;
	public event HuntingEvent OnEndHunting;

	public static EventManager getInstance() {
		if (_eventManager == null) {
			_eventManager = GameObject.FindObjectOfType<EventManager>();
			
			DontDestroyOnLoad(_eventManager.gameObject);
		}
		
		return _eventManager;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartHunting() {
		OnStartHunting();
	}

	public void EndHunting() {
		OnEndHunting();
	}
}
