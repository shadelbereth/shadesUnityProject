using UnityEngine;
using System.Collections;

public class ShadowingMode : MonoBehaviour {

    PlayerManager player;
    bool shadow;
    // Rigidbody body;
    Renderer render;
    Renderer[] renderers;
    Color color;
    float size;
    float particleBySize;

	// Use this for initialization
	void Start () {
	   player = GameObject.Find("Player").GetComponent<PlayerManager>();
       shadow = false;
       // body = GetComponent<Rigidbody>();
       render = GetComponent<Renderer>();
       if (render == null) {
            renderers = GetComponentsInChildren<Renderer>();
       }
        // Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>(), false);
       var shape = GetComponent<ParticleSystem>().shape;
       shape.enabled = true;
       shape.meshShapeType = ParticleSystemMeshShapeType.Triangle;
       if (GetComponent<MeshRenderer>() != null) {
            shape.shapeType = ParticleSystemShapeType.MeshRenderer;
            shape.meshRenderer = GetComponent<MeshRenderer>();
       } else {
            shape.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
            shape.skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
       }
       GetComponent<ParticleSystem>().startSpeed = 0.1f;
       GetComponent<ParticleSystem>().startLifetime = 0.7f;
       color = Color.black;
       color.a = 0.5f;
       GetComponent<ParticleSystem>().startColor = color;
       GetComponent<ParticleSystem>().scalingMode = ParticleSystemScalingMode.Shape;
       Vector3 volume = render.bounds.size;
       size = volume.x * volume.z * volume.y;
       particleBySize = 150;
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
            // GetComponent<Collider>().isTrigger = true;
            // if (body != null) {
            //     body.useGravity = false;
            // }
            // Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>(), true);
            var emission = GetComponent<ParticleSystem>().emission;
            emission.enabled = true;
            emission.rate = size * particleBySize;
            if (render != null) {
                render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                // render.materials[0].SetColor("_Color", color);
            } else {
                foreach (Renderer renderer in renderers) {
                    renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                    // renderer.materials[0].SetColor("_Color", color);
                }
            }
        }
    }

    void GoNormal () {
        if (shadow) {
            shadow = false;
            // GetComponent<Collider>().isTrigger = false;
            // if (body != null) {
            //     body.useGravity = true;
            // }
            var emission = GetComponent<ParticleSystem>().emission;
            emission.enabled = true;
            emission.rate = 0;
            // Physics.IgnoreCollision(GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            if (render != null) {
                render.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                // render.materials[0].SetColor("_Color", Color.white);
            } else {
                foreach (Renderer renderer in renderers) {
                    renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    // renderer.materials[0].SetColor("_Color", Color.white);
                }
            }
        }
    }

    // void OnTriggerStay (Collider other) {
    //     if (other.tag == "Player") {
    //         other.GetComponent<PlayerManager>().DetectWall();
    //     }
    // }
}
