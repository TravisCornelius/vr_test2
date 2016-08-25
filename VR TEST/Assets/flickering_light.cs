using UnityEngine;
using System.Collections;

public class flickering_light : MonoBehaviour {

	public float timer = 5;
	private Light[] lights;
	public GameObject lightParent;

	void Update () { 

		timer -= Time.deltaTime;

		if(timer < 0){
			lights = GetComponentsInChildren<Light>(true);

			foreach (Light light in lights)
			{
				if (GetComponent<Light>().enabled == true){
					GetComponent<Light>().enabled = false; 
				} else if (GetComponent<Light>().enabled == false){
					Debug.Log("I'm flashing you."); 
					GetComponent<Light>().enabled = true; 
				} 
				timer = 5;
			}

		}
	}

}
