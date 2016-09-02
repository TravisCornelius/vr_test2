﻿using UnityEngine;
using System.Collections;

public class Lighting : MonoBehaviour {
    float duration = .40F;
	public Light lt;
	int blinks = 0;
	bool dark = false;
	public AudioClip roboticWave;
	bool triggeredLightEffect = false;
	int numOfBlinks = 5;
	public GameObject ghostRadio;
	public GameObject normalRadio;
	AudioSource ghostSource;
	AudioSource normalSource;

	AudioSource audioSource;

	void Start() {
		lt = GetComponent<Light>();
		audioSource = GetComponent<AudioSource>();
		ghostSource = ghostRadio.GetComponent<AudioSource> ();
		normalSource = normalRadio.GetComponent<AudioSource> ();
	}
	void Update() {
		if (!triggeredLightEffect && PlayerScript.solvedPuzzle1) {
			audioSource.Play ();
			if (blinks < numOfBlinks) {
				float phi = Time.time / duration * 2 * Mathf.PI;
				float amplitude = Mathf.Cos (phi) * 0.5F + 0.5F;
				lt.intensity = amplitude;
				if (!dark && amplitude < .05) {
					dark = true;
					blinks++;
				}
				if (dark && amplitude > .95) {
					dark = false;
				}

				audioSource.volume = amplitude;
			} else {
				
				triggeredLightEffect = true;
				StartCoroutine(lightGhostWorld ());


			}
		}
	}

	private IEnumerator lightGhostWorld()
	{
		Debug.Log ("LIGHT THE GHOST WORLD");
		audioSource.Stop ();

		lt.intensity = 0;
		audioSource.volume = 1;
		audioSource.clip = roboticWave;
		audioSource.Play ();
		//yield return new WaitForSeconds( audioSource.clip.length );

		yield return new WaitForSeconds( 2 );
		normalSource.Stop ();
		ghostSource.Play ();
		PlayerScript.ghostMode = true;
		lt.intensity = 1;
	
	}
}
