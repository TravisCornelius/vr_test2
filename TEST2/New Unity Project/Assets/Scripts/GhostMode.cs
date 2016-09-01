using UnityEngine;
using System.Collections;

public class GhostMode : MonoBehaviour {

	Shader originalShader;
	Shader ghostShader;

	Renderer rend;
    public bool dontRender = false;
	Rigidbody rb;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rend = GetComponent<Renderer>();        
        originalShader = rend.material.shader;
		ghostShader = Shader.Find ("Ciconia Studio/Effects/Ghost/Old version(1.2)/Ghost Animated Details");
		DisablePhysics ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!PlayerScript.ghostMode) {
            if (dontRender)
            {
                rend.enabled = false;
            } else
            {
                rend.material.shader = ghostShader;
				rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }
			DisablePhysics ();
		} else {
            rend.enabled = true;
			rend.material.shader = originalShader;
			rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
			EnablePhysics ();
		}
	}
	void EnablePhysics() {
		rb.isKinematic = false;
		rb.detectCollisions = true;
	}
	void DisablePhysics() {
		rb.isKinematic = true;
		rb.detectCollisions = false;
	}
}
