using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour
{

	public static void Load (string levelName)
	{
		Application.LoadLevel (levelName);
	}

}
