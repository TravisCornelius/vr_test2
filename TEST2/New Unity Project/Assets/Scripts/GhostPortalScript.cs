using UnityEngine;
using System.Collections;

public class GhostPortalScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (PlayerScript.ghostMode) {
			GetComponentInChildren<ParticleSystem>().enableEmission = false;
			GetComponent<BoxCollider> ().enabled = false;
		} else {
			GetComponentInChildren<ParticleSystem>().enableEmission = true;
			GetComponent<BoxCollider> ().enabled = true;
		}
	}
}
