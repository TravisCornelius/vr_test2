using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]

public class PickupParent : MonoBehaviour {

	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device device;

	//public Transform sphere;

	// Use this for initialization
	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		device = SteamVR_Controller.Input((int)trackedObj.index);
		if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			Debug.Log ("You are holding 'touch' on the trigger");
		}
		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			Debug.Log ("You are pressed 'TouchDown' on the trigger");
		}
		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			Debug.Log ("You are pressed 'TouchUp' on the trigger");
		}
		if (device.GetPress (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			Debug.Log ("You are holding 'Press' on the trigger");
		}
		if (device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			Debug.Log ("You are pressed 'PressDown' on the trigger");
		}
		if (device.GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			Debug.Log ("You are pressed 'PressUp' on the trigger");
		}
	}

	void OnTriggerStay (Collider col) {
		Debug.Log("You have collided with " + col.name + " and activated onTriggerStay");
		if (device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			Debug.Log ("You are holding 'touch' on the trigger and collided");
			col.attachedRigidbody.isKinematic = true;
			col.gameObject.transform.SetParent (this.gameObject.transform);
		}
		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			Debug.Log ("You are pressed 'TouchDown' on the trigger and collided");
		}
		if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			Debug.Log ("You are pressed 'TouchUp' on the trigger and collided");
			col.gameObject.transform.SetParent (null);
			col.attachedRigidbody.isKinematic = false;
		}
		if (device.GetPress (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			Debug.Log ("You are holding 'Press' on the trigger and collided");
		}
		if (device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			Debug.Log ("You are pressed 'PressDown' on the trigger and collided");
		}
		if (device.GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) 
		{
			Debug.Log ("You are pressed 'PressUp' on the trigger and collided");
		}
	}

	void tossObject(Rigidbody rigidBody) {
		Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
		if (origin != null) {
			rigidBody.velocity = origin.TransformVector (device.velocity);
			rigidBody.angularVelocity = origin.TransformVector (device.angularVelocity);
		}
		rigidBody.velocity = device.velocity;
		rigidBody.angularVelocity = device.angularVelocity;
	}
}
