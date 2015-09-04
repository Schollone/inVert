using UnityEngine;
using System.Collections;

public class ShootProjectile : MonoBehaviour
{

	public float timeBetweenShots = 4;
	public GameObject projectilePrefab;
	public float spawnOffset = 1;
	public AudioClip[] audioFilesShots;
	AudioSource audioSourceShots;

	void Awake ()
	{
		audioSourceShots = GetComponent<AudioSource> ();
	}

	void Start ()
	{
		InvokeRepeating ("Spawn", timeBetweenShots, timeBetweenShots);
	}

	void Spawn ()
	{
		GameObject projectile = Instantiate (projectilePrefab, transform.position + transform.forward * GetComponent<Collider> ().bounds.size.magnitude,
		             transform.rotation) as GameObject;
		ProjectileLocomotion projectileLoc = projectile.GetComponent<ProjectileLocomotion> ();

		if (GetComponent<AimAtPlayer> ().getAimInFront () == true) {
			projectileLoc.accuracy = 0;
		}

		PlaySound ();
	}

	void PlaySound ()
	{
		audioSourceShots.clip = audioFilesShots [Random.Range (0, audioFilesShots.Length)];
		audioSourceShots.Play ();

	}

}
