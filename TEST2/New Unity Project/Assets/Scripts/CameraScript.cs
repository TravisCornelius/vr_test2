using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("start");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter (Collision col)
	{
		
		if(col.gameObject.name == "ZombiePortal")
		{
			Debug.Log ("Collision");

			Application.LoadLevel ("Zombie");
		}
	}
}
