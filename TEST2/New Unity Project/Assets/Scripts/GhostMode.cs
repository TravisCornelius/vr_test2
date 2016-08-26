using UnityEngine;
using System.Collections;

public class GhostMode : MonoBehaviour {

	Shader originalShader;
	Shader ghostShader;

	Renderer rend;
    MeshRenderer meshRend;
    public bool dontRender = false;


	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
        meshRend = GetComponent<MeshRenderer>();
        originalShader = rend.material.shader;
		ghostShader = Shader.Find ("Ciconia Studio/Effects/Ghost/Old version(1.2)/Ghost Animated Details");
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerScript.ghostMode) {
            if (dontRender)
            {
                meshRend.enabled = false;
            } else
            {
                rend.material.shader = ghostShader;
            }
			
		} else {
            meshRend.enabled = true;
			rend.material.shader = originalShader;
		}
	}
}
