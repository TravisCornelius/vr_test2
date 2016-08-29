 using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

	public static bool ghostMode = false;
    public bool viewGhost = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //viewGhost = ghostMode;
        ghostMode = viewGhost;
	}

	void OnCollisionEnter (Collision col)
	{
        Debug.Log(col.gameObject.name);
		if (col.gameObject.name == "ZombiePortal")
		{
            SceneManager.LoadScene("Zombie");
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
