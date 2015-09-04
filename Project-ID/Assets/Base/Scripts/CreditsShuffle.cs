using UnityEngine;
using System.Collections;

public class CreditsShuffle : MonoBehaviour {

	public TextMesh credits;
	public string[] names;
	
	void Start () {
		Shuffle (names);
		Shuffle (names);
		credits.text = "developed by:\n\n"+names[0]+"\n"+names[1]+"\n"+names[2]+"\n"+names[3];
	}

	void Shuffle(string[] a)
	{
		for (int i = a.Length-1; i > 0; i--)
		{
			int rnd = Random.Range(0,i);
			string temp = a[i];
			a[i] = a[rnd];
			a[rnd] = temp;
		}
	}
}
