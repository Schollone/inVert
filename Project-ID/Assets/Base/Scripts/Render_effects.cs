using UnityEngine;
using System.Collections;

public class Render_effects : MonoBehaviour
{
	public float FStartDistance;
	public float FEndDistance;
	// Use this for initialization
	void Start ()
	{
	 
	}
	
	// Update is called once per frame
	void Update ()
	{
		RenderSettings.fog = true;
		RenderSettings.fogColor = Color.white;
		RenderSettings.fogMode = FogMode.Linear;
		RenderSettings.fogEndDistance = FEndDistance;
		RenderSettings.fogStartDistance = FStartDistance;
	}
}
