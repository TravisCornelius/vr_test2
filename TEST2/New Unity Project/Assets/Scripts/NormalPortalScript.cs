using UnityEngine;
using System.Collections;

public class NormalPortalScript : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerScript.ghostMode) {
			GetComponentInChildren<ParticleSystem>().enableEmission = true;
			GetComponent<BoxCollider> ().enabled = true;
		} else {
			GetComponentInChildren<ParticleSystem>().enableEmission = false;
			GetComponent<BoxCollider> ().enabled = false;
		}
	}
}
