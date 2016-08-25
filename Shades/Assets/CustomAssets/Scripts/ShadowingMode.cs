using UnityEngine;
using System.Collections;

public class ShadowingMode : MonoBehaviour {

    PlayerManager player;
    bool shadow;
    Rigidbody body;
    Renderer render;
    Renderer[] renderers;

	// Use this for initialization
	void Start () {
	   player = GameObject.Find("Player").GetComponent<PlayerManager>();
       shadow = false;
       body = GetComponent<Rigidbody>();
       render = GetComponent<Renderer>();
       if (render == null) {
            renderers = GetComponentsInChildren<Renderer>();
       }
	}
	
	// Update is called once per frame
	void Update () {
	   if (player.IsShadowing()) {
            GoShade();
       } else {
            GoNormal();
       }
	}

    void GoShade () {
        if (! shadow) {
            shadow = true;
            GetComponent<Collider>().isTrigger = true;
            if (body != null) {
                body.useGravity = false;
            }
            if (render != null) {
                render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                render.materials[0].SetColor("_Color", Color.black);
            } else {
                foreach (Renderer renderer in renderers) {
                    // renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                    renderer.materials[0].SetColor("_Color", Color.black);
                }
            }
        }
    }

    void GoNormal () {
        if (shadow) {
            shadow = false;
            GetComponent<Collider>().isTrigger = false;
            if (body != null) {
                body.useGravity = true;
            }
            if (render != null) {
                render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                render.materials[0].SetColor("_Color", Color.white);
            } else {
                foreach (Renderer renderer in renderers) {
                    // renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    renderer.materials[0].SetColor("_Color", Color.white);
                }
            }
        }
    }

    void OnTriggerStay (Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<PlayerManager>().DetectWall();
        }
    }
}
