 using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public static bool ghostMode = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.name == "ZombiePortal")
		{
			Application.LoadLevel ("Zombie");
		} 
		if(col.gameObject.name == "NormalPortal")
		{
			ghostMode = false;
		}
		if(col.gameObject.name == "GhostPortal")
		{
			ghostMode = true;
		}
	}
}
