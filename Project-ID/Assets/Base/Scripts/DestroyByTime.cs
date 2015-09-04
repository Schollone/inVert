using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{
	public float lifetime = 1;

	private void Start ()
	{
		Invoke ("DestroySelf", lifetime);
	}

	private void DestroySelf ()
	{
		DestroyObject (this.gameObject);
	}
}
