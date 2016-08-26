using UnityEngine;
using System.Collections;

public class NormalMode : MonoBehaviour {

	Shader originalShader;
	Shader ghostShader;
	Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		originalShader = rend.material.shader;
		ghostShader = Shader.Find ("Ciconia Studio/Effects/Ghost/Old version(1.2)/Ghost Animated Details");
	}
	
	// Update is called once per frame
	void Update () {
		if (!PlayerScript.ghostMode) {
			rend.material.shader = ghostShader;
		} else {
			rend.material.shader = originalShader;
		}
	}
}
