/// <summary>
/// Portal.cs
/// David Flynn
/// 
/// 
/// To make less Locations and buttons avaible type // befor any of the locations and buttons you do not want.
/// To add locations you must duplicate a location line and change it to the next number you must also do this for the btton line.
/// Then you must go down a duplicate a If line in bottom half and change it to match the same names as your added location and buttons.
/// </summary>

using UnityEngine;
using System.Collections;


public class Portal : MonoBehaviour {
	
	bool showMessage1 = false;// This makes it so you only show the message when you are in the trigger.
	
	public GameObject location1;// Opens it up so you can set location 1 in Inspector.
	public GameObject location2;// Opens it up so you can set location 2 in Inspector.Type // before this to change remove it from scene.
	public GameObject location3;// Opens it up so you can set location 3 in Inspector.Type // before this to change remove it from scene.
	public GameObject location4;// Opens it up so you can set location 4 in Inspector.Type // before this to change remove it from scene.
	public GameObject location5;// Opens it up so you can set location 5 in Inspector.Type // before this to change remove it from scene.
	public GameObject location6;// Opens it up so you can set location 6 in Inspector.Type // before this to change remove it from scene.
	
	private string backgroundText = " ";//Allows you to set text for the background.
	
	public GUISkin backgroundTexture;//Allows you to set your background GUIskin in the Inspector
	public GUISkin backgroundTitle;//Allows you to set your background GUIskin in the Inspector
	
	public GUISkin Location1Button;//Allows you to set the buttons 1 texture in the Inspector.
	public GUISkin Location2Button;//Allows you to set the buttons 2 texture in the Inspector.  Type // before this to change remove it from scene.
	public GUISkin Location3Button;//Allows you to set the buttons 3 texture in the Inspector.  Type // before this to change remove it from scene.
	public GUISkin Location4Button;//Allows you to set the buttons 4 texture in the Inspector.  Type // before this to change remove it from scene.
	public GUISkin Location5Button;//Allows you to set the buttons 5 texture in the Inspector.  Type // before this to change remove it from scene.
	public GUISkin Location6Button;//Allows you to set the buttons 6 texture in the Inspector.  Type // before this to change remove it from scene.
	


	public void OnTriggerEnter(Collider other)//Checks to see if somthing has entered the colldier.
	{
		if(other.transform.CompareTag("Player"))
		{
		showMessage1 = true;// If in trigger show Message.	
		}	
		
	}
	
	public void OnTriggerExit(Collider other)//Checks to see if somthing has exited the colldier.
	{
		
		if(other.transform.CompareTag("Player"))
		{
		showMessage1 = false;//If not in trigger do not show Message.
		}
	}
	
	 void OnGUI()
	{
	  if (showMessage1)//checks to see what the status of the message is.
		{
			//creates the background that you place your image on.
		    GUI.skin = backgroundTexture;  
			GUI.Box(new Rect(150, 45, 500, 500), backgroundText);
			
			GUI.skin = backgroundTitle;
			GUI.Box(new Rect(250, 5, 300, 125), backgroundText);
			
			//Put a // infront of any button you dont want to show on GUI.
			
			GUI.skin = Location1Button;
			  if (GUI.Button(new Rect(350, 100, 110, 55),backgroundText)) //Button 1 for teleporter.
				{
					
					showMessage1 = false;//after you click the button it hides the message.
				
					GameObject player = GameObject.FindWithTag("Player");//Looks for a tag of Player	
		  			player.transform.position = location1.transform.position;
				
				}
			
			GUI.skin = Location2Button;
			  if (GUI.Button(new Rect(350, 160, 110, 55),backgroundText)) //Button 2 for teleporter.
				{
			
		  			showMessage1 = false;//after you click the button it hides the message.
					GameObject player = GameObject.FindWithTag("Player");//Looks for a tag of Player	
		  			player.transform.position = location2.transform.position;//Changes Player Position.
				}
			GUI.skin = Location3Button;
			  if (GUI.Button(new Rect(350, 220, 110, 55), backgroundText)) //Button 3 for teleporter.
				{
			
		  			showMessage1 = false;//after you click the button it hides the message.
					GameObject player = GameObject.FindWithTag("Player");//Looks for a tag of Player	
		  			player.transform.position = location3.transform.position;//Changes Player Position.
				}
			GUI.skin = Location4Button;
			  if (GUI.Button(new Rect(350, 280, 110, 55), backgroundText)) //Button 4 for teleporter.
				{
			
		  			showMessage1 = false;
					GameObject player = GameObject.FindWithTag("Player");//Looks for a tag of Player	
		  			player.transform.position = location4.transform.position;//Changes Player Position.
				}
			GUI.skin = Location5Button;
			  if (GUI.Button(new Rect(350, 340, 110, 55), backgroundText)) //Button 5 for teleporter.
				{
			
		  			showMessage1 = false;//after you click the button it hides the message.
					GameObject player = GameObject.FindWithTag("Player");//Looks for a tag of Player	
		  			player.transform.position = location5.transform.position;//Changes Player Position.
				}
			GUI.skin = Location6Button;
			  if (GUI.Button(new Rect(350, 400, 110, 55),backgroundText)) //Button 6 for teleporter.
				{
			
		  			showMessage1 = false;//after you click the button it hides the message.
					GameObject player = GameObject.FindWithTag("Player");//Looks for a tag of Player	
		  			player.transform.position = location6.transform.position;//Changes Player Position.
				}
		}
		
	}
}
	

