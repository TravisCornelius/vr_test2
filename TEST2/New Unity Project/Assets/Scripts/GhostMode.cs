using UnityEngine;
using System.Collections;

public class GhostMode : MonoBehaviour {

	Shader originalShader;
	Shader ghostShader;

	Renderer rend;
    SkinnedMeshRenderer meshRend;
    public bool dontRender = false;


	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
        meshRend = GetComponent<SkinnedMeshRenderer>();
        originalShader = rend.material.shader;
        Debug.Log(originalShader);
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
