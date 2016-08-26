using UnityEngine;
using System.Collections;

public class Energy_Controller : MonoBehaviour {

	public GameObject effect1;// Opens it up so you can set effect 1 in Inspector.
	public GameObject effect2;// Opens it up so you can set effect 2 in Inspector.
	
	
	public void Start()
		
	{
		effect1.SetActive (true);
		effect2.SetActive (false);
		
	}
	
	public void OnTriggerEnter(Collider other)//Checks to see if somthing has entered the colldier.
	{
		if(other.transform.CompareTag("Player"))
		{
			effect1.SetActive (false);
			effect2.SetActive (true);
		}	
		
	}
	
	public void OnTriggerExit(Collider other)//Checks to see if somthing has exited the colldier.
	{
		
		if(other.transform.CompareTag("Player"))
		{
			effect1.SetActive (true);
			effect2.SetActive (false);
		}
	}
}
