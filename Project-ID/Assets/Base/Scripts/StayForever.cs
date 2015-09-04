using UnityEngine;
using System.Collections;

public class StayForever : MonoBehaviour
{


	void Start ()
	{
		DontDestroyOnLoad (this.gameObject);
	}

}
